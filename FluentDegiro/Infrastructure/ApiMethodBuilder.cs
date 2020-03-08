using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;

namespace FluentDegiro.Infrastructure
{
    /// <summary>
    /// Builder-style implementation of the abstract ApiMethodBase that is created using a "parent" IRequestBuilderFactory providing 
    /// the context this method is called in. The specific attributes of this api method can be configured using the builder-style 
    /// api this class provides.
    /// </summary>
    internal class ApiMethodBuilder : ApiMethodBase, IRequestBuilderFactory
    {
        private protected IRequestBuilderFactory Context { get; }

        protected dynamic QueryStringParameters { get; } = new ExpandoObject();
        protected IDictionary<string, object> QueryStringParametersDict => (IDictionary<string, object>)QueryStringParameters;

        protected object Body { get; }

        protected Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        protected HttpMethod Method { get; set; }

        internal ApiMethodBuilder(IRequestBuilderFactory context)
        {
            Context = context;
        }

        protected ApiMethodBuilder(IRequestBuilderFactory context, ExpandoObject queryStringParameters, object body) : this(context)
        {
            if (queryStringParameters != null)
                QueryStringParameters = queryStringParameters;
            Body = body;
        }

        IRequestBuilder IRequestBuilderFactory.CreateRequestBuilder()
            => CreateRequestBuilder();

        protected override IRequestBuilder CreateRequestBuilder()
        {
            var builder = Context.CreateRequestBuilder();
            builder.Method = Method ?? HttpMethod.Get;
            builder.QueryStringParameters.SetValues((ExpandoObject)QueryStringParameters);
            if (Body != null) {
                if (builder.Body != null)
                    throw new InvalidOperationException("This request's body is already set.");
                builder.Body = Body;
            }
            builder.Headers.SetValues(Headers);
            return builder;
        }

        internal static ApiMethodBuilder Create(IRequestBuilderFactory context, ExpandoObject queryStringParameters = null, object body = null)
            => Create(context, HttpMethod.Get, queryStringParameters, body);

        internal static ApiMethodBuilder Create(
            IRequestBuilderFactory context,
            HttpMethod method,
            ExpandoObject queryStringParameters = null,
            object body = null)
        {
            var meth = new ApiMethodBuilder(context, queryStringParameters, body)
            {
                Method = method
            };
            return meth;
        }
    }
}
