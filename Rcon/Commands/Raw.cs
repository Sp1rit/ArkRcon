namespace Rcon.Commands
{
    public class Raw : Command, ICommand
    {
        public string Command { get; set; }

        public Raw()
            : base(CommandType.Raw)
        { }

        public Raw(string command)
            : this()
        {
            Command = command;
        }

        public bool ValidateResponse(string responseBody)
        {
            return true;
        }
        public override string ToString()
        {
            return Command;
        }
    }
}
