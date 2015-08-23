namespace Rcon.Commands
{
    public class ServerChatTo : Command, ICommand
    {
        private const string Command = "ServerChatTo";

        public string SteamId { get; set; }

        public string Message { get; set; }

        public ServerChatTo()
            :base(CommandType.ServerChatTo)
        { }
        
        public ServerChatTo(string steamId, string message)
            :this()
        {
            SteamId = steamId;
            Message = message;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!";
        }

        public override string ToString()
        {
            return $"{Command} \"{SteamId}\" Private Message - {Message}";
        }
    }
}
