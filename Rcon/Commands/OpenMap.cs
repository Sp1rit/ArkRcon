namespace Rcon.Commands
{
    // TODO: Long timeout
    public class OpenMap : Command, ICommand
    {
        private const string Command = "OpenMap";

        public string MapName { get; set; }

        public OpenMap()
            : base(CommandType.OpenMap)
        { }

        public OpenMap(string mapName)
            : this()
        {
            MapName = mapName;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"Opening Map... {MapName}";
        }

        public override string ToString()
        {
            return $"{Command} {MapName}";
        }
    }
}
