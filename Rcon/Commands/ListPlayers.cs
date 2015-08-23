using System.Text.RegularExpressions;

namespace Rcon.Commands
{
    public class ListPlayers : Command, ICommand
    {
        private static readonly Regex PlayerMatch = new Regex(@"[0-9]\. (?<Name>.*), (?<SteamId>[0-9]{17})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private const string Command = "ListPlayers";

        public ListPlayers()
            :base(CommandType.ListPlayers)
        { }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == "No Players Connected" || responseBody.Trim().Length > 5;
        }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}
