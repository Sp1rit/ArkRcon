namespace Rcon.Commands
{
    // TODO: Doesn't work yet!
    public class ForcePlayerToJoinTribe : Command
    {
        private const string Command = "ForcePlayerToJoinTribe";

        public string SteamId { get; set; }

        public string TribeName { get; set; }

        public ForcePlayerToJoinTribe()
            : base(CommandType.ForcePlayerToJoinTribe)
        { }

        public ForcePlayerToJoinTribe(string steamId, string tribeName)
            : this()
        {
            SteamId = steamId;
            TribeName = tribeName;
        }

        public override string ToString()
        {
            return $"{Command} {SteamId} \"{TribeName}\"";
        }
    }
}
