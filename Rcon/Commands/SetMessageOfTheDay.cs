namespace Rcon.Commands
{
    public class SetMessageOfTheDay : Command, ICommand
    {
        private const string Command = "SetMessageOfTheDay";

        public string Message { get; set; }

        public SetMessageOfTheDay()
            : base(CommandType.SetMessageOfTheDay)
        { }

        public SetMessageOfTheDay(string message)
            : this()
        {
            Message = message;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"Message of set to {Message}";
        }

        public override string ToString()
        {
            return $"{Command} {Message}";
        }
    }
}
