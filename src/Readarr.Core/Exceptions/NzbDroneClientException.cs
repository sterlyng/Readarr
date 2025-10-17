using System;
using System.Net;
using Readarr.Common.Exceptions;

namespace Readarr.Core.Exceptions
{
    public class ReadarrClientException : ReadarrException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ReadarrClientException(HttpStatusCode statusCode, string message, params object[] args)
            : base(message, args)
        {
            StatusCode = statusCode;
        }

        public ReadarrClientException(HttpStatusCode statusCode, string message, Exception innerException, params object[] args)
            : base(message, innerException, args)
        {
            StatusCode = statusCode;
        }

        public ReadarrClientException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
