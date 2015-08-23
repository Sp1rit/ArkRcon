using Rcon.Commands;

namespace Rcon
{
    public class CommandExecutedEventArgs
    {
        public bool Successful { get; set; }

        public string Error { get; set; }

        public string Response { get; set; }

        public Command Command { get; set; }
    }
}
