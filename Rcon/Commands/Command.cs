namespace Rcon.Commands
{
    public class Command
    {
        public CommandType Type { get; set; }

        public Command(CommandType type)
        {
            Type = type;
        }
    }
}
