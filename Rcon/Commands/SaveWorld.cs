namespace Rcon.Commands
{
    public class SaveWorld : Command, ICommand
    {
        private const string Command = "SaveWorld";

        public SaveWorld()
            :base(CommandType.SaveWorld)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "World Saved";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
