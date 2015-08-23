namespace Rcon.Commands
{
    // TODO: Doesn't work yet!
    public class KillPlayer : Command
    {
        private const string Command = "KillPlayer";

        public string SteamId { get; set; }

        public KillPlayer()
            :base(CommandType.KillPlayer)
        { }

        public KillPlayer(string steamId)
            : this()
        {
            SteamId = steamId;
        }

        public override string ToString()
        {
            return $"{Command} {SteamId}";
        }
    }
}
