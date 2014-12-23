using System;

namespace Krkadoni.Enigma
{
    internal class TimeOutException : WebRequestException
    {
        public TimeOutException(string message, string url, int timeOut) : base(message)
        {
            Url = url;
            TimeOut = timeOut;
        }

        public TimeOutException(string message, string url, int timeOut, Exception innerException) : base(message, innerException)
        {
            Url = url;
            Timeout = timeOut;
        }

        public string Url { get; protected set; }
        public int Timeout { get; set; }

        public int TimeOut { get; protected set; }
    }
}