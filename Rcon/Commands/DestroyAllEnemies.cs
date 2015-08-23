namespace Rcon.Commands
{
    public class DestroyAllEnemies : Command, ICommand
    {
        private const string Command = "DestroyAllEnemies";

        public DestroyAllEnemies()
            :base(CommandType.DestroyAllEnemies)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "All Enemies Destroyed";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
