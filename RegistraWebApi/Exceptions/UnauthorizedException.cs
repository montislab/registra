using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string AuthorizationFailureInfo { get; private set; }

        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message)
        {
            AuthorizationFailureInfo = message;
        }

        public UnauthorizedException(string message, System.Exception inner) : base(message, inner)
        {
            AuthorizationFailureInfo = message;
        }
    }
}
