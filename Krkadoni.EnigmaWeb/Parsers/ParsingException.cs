using System;

namespace Krkadoni.Enigma.Parsers
{
    public class ParsingException : KnownException
    {
        public ParsingException(string message) : base(message)
        {
        }

        public ParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}