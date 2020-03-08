using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace FluentDegiro.Infrastructure
{
    public class ResponseDescribesProblemException<TContent> : NoSuccessStatusCodeException
    {
        public TContent ProblemDescription { get; }

        public ResponseDescribesProblemException()
        {
        }

        public ResponseDescribesProblemException(string message) : base(message)
        {
        }

        public ResponseDescribesProblemException(string message, HttpStatusCode statusCode, string responseContent, TContent problemDescription) : base(message, statusCode, responseContent)
        {
            ProblemDescription = problemDescription;
        }

        public ResponseDescribesProblemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResponseDescribesProblemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
