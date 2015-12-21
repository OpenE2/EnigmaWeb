using System;
using System.Net;

namespace Krkadoni.Enigma
{
    public class FailedStatusCodeException : KnownException
    {
        public HttpStatusCode StatusCode { get; private set;}

        public FailedStatusCodeException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
            
        public FailedStatusCodeException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}