namespace Rcon.Commands
{
    // TODO: Doesn't work yet!
    public class EnableSpectator : Command
    {
        private const string Command = "EnableSpectator";

        public EnableSpectator()
            :base(CommandType.EnableSpectator)
        { }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
