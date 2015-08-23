namespace Rcon.Commands
{
    public class DisallowPlayerToJoinNoCheck : Command, ICommand
    {
        private const string Command = "DisallowPlayerToJoinNoCheck";

        public string SteamId { get; set; }

        public DisallowPlayerToJoinNoCheck()
            : base(CommandType.DisallowPlayerToJoinNoCheck)
        { }

        public DisallowPlayerToJoinNoCheck(string steamId)
            : this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Disallowed Player To Join No Checknned";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}
