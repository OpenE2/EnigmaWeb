using System;

namespace Krkadoni.Enigma
{
    public abstract class KnownException : Exception
    {
        protected KnownException(string message) : base(message)
        {
        }

        protected KnownException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}