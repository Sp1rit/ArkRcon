namespace Rcon.Commands
{
    public class RenameTribe : Command, ICommand
    {
        private const string Command = "RenameTribe";

        public string TribeName { get; set; }

        public string NewName { get; set; }

        public RenameTribe()
            : base(CommandType.RenameTribe)
        { }

        public RenameTribe(string tribeName, string newName)
            : this()
        {
            TribeName = tribeName;
            NewName = newName;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "Server received, But no response!!";
        }

        public override string ToString()
        {
            return $"{Command} \"{TribeName}\" \"{NewName}\"";
        }
    }
}
