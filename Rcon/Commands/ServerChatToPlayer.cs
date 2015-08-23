namespace Rcon.Commands
{
    public class ServerChatToPlayer : Command, ICommand
    {
        private const string Command = "ServerChatToPlayer";

        public string PlayerName { get; set; }

        public string Message { get; set; }

        public ServerChatToPlayer()
            :base(CommandType.ServerChatToPlayer)
        { }

        public ServerChatToPlayer(string playerName, string message)
            : this()
        {
            PlayerName = playerName;
            Message = message;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!";
        }

        public override string ToString()
        {
            return $"{Command} \"{PlayerName}\" {Message}";
        }
    }
}
