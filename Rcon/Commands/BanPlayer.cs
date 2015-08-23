namespace Rcon.Commands
{
    public class BanPlayer : Command, ICommand
    {
        private const string Command = "BanPlayer";

        public string SteamId { get; set; }

        public BanPlayer()
            :base(CommandType.BanPlayer)
        { }

        public BanPlayer(string steamId)
            :this()
        {
            SteamId = steamId;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"{SteamId} Banned";
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}
