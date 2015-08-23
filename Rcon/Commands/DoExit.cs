namespace Rcon.Commands
{
    public class DoExit : Command, ICommand
    {
        private const string Command = "DoExit";

        public DoExit()
            :base(CommandType.DoExit)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Exiting...";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
