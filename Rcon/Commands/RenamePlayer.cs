namespace Rcon.Commands
{
    public class RenamePlayer : Command, ICommand
    {
        private const string Command = "RenamePlayer";

        public string PlayerName { get; set; }

        public string NewName { get; set; }

        public RenamePlayer()
            : base(CommandType.RenamePlayer)
        { }

        public RenamePlayer(string playerName, string newName)
            : this()
        {
            PlayerName = playerName;
            NewName = newName;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!";
        }

        public override string ToString()
        {
            return $"{Command} \"{PlayerName}\" \"{NewName}\"";
        }
    }
}
