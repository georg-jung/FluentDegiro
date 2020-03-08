using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Infrastructure
{
    /// <summary>
    /// Basic implementation of the abstract ApiMethodBase that can be used directly, given an IRequestBuilder or a factory creating one.
    /// </summary>
    internal class ApiMethod : ApiMethodBase
    {
        private readonly IRequestBuilder requestBuilder;
        private readonly Func<IRequestBuilder> builderFactory;

        internal ApiMethod(IRequestBuilder requestBuilder)
        {
            this.requestBuilder = requestBuilder;
        }

        internal ApiMethod(IRequestBuilderFactory factory) : this(factory.CreateRequestBuilder)
        {
        }

        internal ApiMethod(Func<IRequestBuilder> builderFactory)
        {
            this.builderFactory = builderFactory;
        }

        protected override IRequestBuilder CreateRequestBuilder()
            => requestBuilder ?? builderFactory();
    }
}
