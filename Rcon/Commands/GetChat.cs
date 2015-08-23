namespace Rcon.Commands
{
    public class GetChat : Command, ICommand
    {
        private const string Command = "GetChat";

        public GetChat()
            : base(CommandType.GetChat)
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
