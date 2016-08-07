namespace Rcon.Commands
{
    public class GetGameLog : Command, ICommand
    {
        private const string Command = "GetGameLog";

        public GetGameLog()
            : base(CommandType.GetGameLog)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!" || responseBody.Trim().Length > 0;
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
