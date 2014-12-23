using System;

namespace Krkadoni.Enigma
{
    public class WebRequestException : KnownException
    {
        public WebRequestException(string message) : base(message)
        {
        }

        public WebRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}