using System;

namespace Krkadoni.Enigma.Commands
{
    public class CommandException : KnownException
    {
        public CommandException(string message) : base(message)
        {
        }

        public CommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}