using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using ConcurrentPriorityQueue;
using Rcon.Commands;

namespace Rcon
{
    public class RconClient
    {
        #region Events

        public event EventHandler Connecting;
        public event EventHandler Connected;
        public event EventHandler<string> ConnectionFailed;
        public event EventHandler<bool> Disconnected;
        public event EventHandler<CommandExecutedEventArgs> CommandExecuted;

        #endregion // Events

        private readonly ConcurrentPriorityQueue<KeyValuePair<Command, EventHandler<CommandExecutedEventArgs>>, int> queue;
        private readonly ManualResetEvent resetEvent;
        private readonly RconBase rcon;

        public bool IsConnected => rcon.Connected;

        public RconClient()
        {
            queue = new ConcurrentPriorityQueue<KeyValuePair<Command, EventHandler<CommandExecutedEventArgs>>, int>();
            resetEvent = new ManualResetEvent(false);
            Thread workerThread = new Thread(ExecuteCommandWorker);
            workerThread.IsBackground = true;
            workerThread.Start();
            rcon = new RconBase();
        }

        public bool Connect(string host, int port, string password)
        {
            Connecting?.Invoke(this, EventArgs.Empty);

            try
            {
                if (!rcon.Connect(host, port))
                    throw new Exception("Connection to server failed. Check your host and port.");

                if (!rcon.Authenticate(password))
                {
                    Disconnect();
                    throw new Exception("Authentication with server failed. The password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                ConnectionFailed?.Invoke(this, ex.Message);
                throw;
            }

            Connected?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void Disconnect(bool requested = true)
        {
            resetEvent.Reset();
            lock (queue)
                queue.Clear();

            rcon.Disconnect();
            Disconnected?.Invoke(this, requested);
        }

        public CommandExecutedEventArgs ExecuteCommand(Command command)
        {
            try
            {
                RconPacket request = new RconPacket(PacketType.ServerdataExeccommand, command.ToString());
                RconPacket response = rcon.SendReceive(request);

                if (request.Id != response.Id)
                    throw new Exception("Got a response with a wrong ID!");

                return new CommandExecutedEventArgs()
                {
                    Successful = response != null,
                    Error = "",
                    Response = response?.Body.Trim(),
                    Command = command
                };
            }
            catch (SocketException sEx)
            {
                Disconnect(false);

                return new CommandExecutedEventArgs()
                {
                    Successful = false,
                    Error = sEx.Message,
                    Response = "",
                    Command = command
                };
            }
            catch (Exception ex)
            {
                return new CommandExecutedEventArgs()
                {
                    Successful = false,
                    Error = ex.Message,
                    Response = "",
                    Command = command
                };
            }
        }

        public void ExecuteCommandAsync(Command command, EventHandler<CommandExecutedEventArgs> callback)
        {
            lock (queue)
                queue.Enqueue(new KeyValuePair<Command, EventHandler<CommandExecutedEventArgs>>(command, callback), 5);

            resetEvent.Set();
        }

        public void ExecuteLowPrioCommandAsync(Command command, EventHandler<CommandExecutedEventArgs> callback)
        {
            lock (queue)
            {
                if (!queue.Any(q => q.Key.Type != command.Type))
                {
                    queue.Enqueue(new KeyValuePair<Command, EventHandler<CommandExecutedEventArgs>>(command, callback), 1);
                    resetEvent.Set();
                }
            }
        }

        private void ExecuteCommandWorker()
        {
            try
            {
                while (true)
                {
                    if (queue.Count > 0)
                    {
                        KeyValuePair<Command, EventHandler<CommandExecutedEventArgs>> entry;
                        lock (queue)
                            entry = queue.Dequeue();

                        try
                        {
                            RconPacket request = new RconPacket(PacketType.ServerdataExeccommand, entry.Key.ToString());
                            RconPacket response = rcon.SendReceive(request);

                            if (request.Id != response.Id)
                                throw new Exception("Got a response with a wrong ID!");

                            var commandExecutedEventArgs = new CommandExecutedEventArgs()
                            {
                                Successful = response != null,
                                Error = "",
                                Response = response?.Body.Trim(),
                                Command = entry.Key
                            };
                            entry.Value?.Invoke(this, commandExecutedEventArgs);
                            CommandExecuted?.Invoke(this, commandExecutedEventArgs);
                        }
                        catch(SocketException sEx)
                        {
                            Disconnect(false);

                            var commandExecutedEventArgs = new CommandExecutedEventArgs()
                            {
                                Successful = false,
                                Error = sEx.Message,
                                Response = "",
                                Command = entry.Key
                            };
                            entry.Value?.Invoke(this, commandExecutedEventArgs);
                            CommandExecuted?.Invoke(this, commandExecutedEventArgs);
                        }
                        catch (Exception ex)
                        {
                            var commandExecutedEventArgs = new CommandExecutedEventArgs()
                            {
                                Successful = false,
                                Error = ex.Message,
                                Response = "",
                                Command = entry.Key
                            };
                            entry.Value?.Invoke(this, commandExecutedEventArgs);
                            CommandExecuted?.Invoke(this, commandExecutedEventArgs);
                        }
                    }

                    if (queue.Count == 0)
                        resetEvent.Reset();

                    resetEvent.WaitOne();
                }
            }
            catch(ThreadAbortException tEx)
            {

            }
        }
    }
}
