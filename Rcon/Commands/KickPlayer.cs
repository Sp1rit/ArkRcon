namespace Rcon.Commands
{
    public class KickPlayer : Command, ICommand
    {
        private const string Command = "KickPlayer";

        public string SteamId { get; set; }

        public KickPlayer()
            :base(CommandType.KickPlayer)
        { }

        public KickPlayer(string steamId)
            : this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Kicked";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}
