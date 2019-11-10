using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RegistraWebApi.Exceptions
{
    public class ErrorStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ErrorStatusCodeException() : base() { }

        public ErrorStatusCodeException(HttpStatusCode statusCode) : base("Status Code: " + statusCode.ToString())
        {
            StatusCode = statusCode;
        }

        public ErrorStatusCodeException(HttpStatusCode statusCode, System.Exception inner) : base("Status Code: " + statusCode.ToString(), inner)
        {
            StatusCode = statusCode;
        }
    }
}
