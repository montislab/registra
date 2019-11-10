using System;

namespace RegistraWebApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public string BadRequestInfo { get; private set; }

        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) 
        {
            BadRequestInfo = message;
        }

        public BadRequestException(string message, System.Exception inner) : base(message, inner)
        {
            BadRequestInfo = message;
        }
    }
}
