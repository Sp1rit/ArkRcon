using System;
using Rcon.Commands;

namespace Rcon
{
    static class CommandFactory
    {
        public static T Create<T>() where T : Command
        {
            return Activator.CreateInstance<T>();
        }
    }
}
