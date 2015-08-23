using System;

namespace Rcon.Commands
{
    public class SetTimeOfDay : Command, ICommand
    {
        private const string Command = "SetTimeOfDay";

        public TimeSpan Time { get; set; }

        public SetTimeOfDay()
            : base(CommandType.SetTimeOfDay)
        { }

        public SetTimeOfDay(TimeSpan time)
            : this()
        {
            Time = time;
        }

        public bool ValidateResponse(string responseBody)
        {
            return responseBody.Trim() == $"Time of day has been set to {Time.ToString("HH:mm:ss")}";
        }

        public override string ToString()
        {
            return $"{Command} {Time.ToString("hh\\:mm\\:ss")}";
        }
    }
}
