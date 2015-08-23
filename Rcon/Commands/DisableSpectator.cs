namespace Rcon.Commands
{
    // TODO: Doesn't work yet!
    public class DisableSpectator : Command
    {
        private const string Command = "DisableSpectator";

        public DisableSpectator()
            :base(CommandType.DisableSpectator)
        { }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
