namespace Rcon.Commands
{
    public class DestroyWildDinos : Command, ICommand
    {
        private const string Command = "DestroyWildDinos";

        public DestroyWildDinos()
            :base(CommandType.DestroyWildDinos)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "All Wild Dinos Destroyed";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
