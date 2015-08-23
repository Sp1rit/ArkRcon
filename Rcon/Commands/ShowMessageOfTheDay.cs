namespace Rcon.Commands
{
    public class ShowMessageOfTheDay : Command, ICommand
    {
        private const string Command = "ShowMessageOfTheDay";

        public ShowMessageOfTheDay()
            :base(CommandType.ShowMessageOfTheDay)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Message of the day Shown";
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
