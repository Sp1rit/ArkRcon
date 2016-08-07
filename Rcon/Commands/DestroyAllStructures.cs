namespace Rcon.Commands
{
    public class DestroyStructures : Command, ICommand
    {
        private const string Command = "DestroyStructures";

        public DestroyStructures()
            :base(CommandType.DestroyStructures)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "All Structures Destroyed";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
