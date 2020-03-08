using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace FluentDegiro.Infrastructure
{
    public class NoSuccessStatusCodeException : ApiException
    {
        public HttpStatusCode StatusCode { get; }
        public string ResponseContent { get; }

        public NoSuccessStatusCodeException()
        {
        }

        public NoSuccessStatusCodeException(string message) : base(message)
        {
        }

        public NoSuccessStatusCodeException(string message, HttpStatusCode statusCode, string responseContent) : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        public NoSuccessStatusCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuccessStatusCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
