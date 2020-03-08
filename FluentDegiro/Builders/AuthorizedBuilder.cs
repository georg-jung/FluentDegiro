using FluentDegiro.Abstractions.Builders;
using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Builders;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Builders
{
    internal class AuthorizedBuilder : IAuthorizedSyntax, IRequestBuilderFactory
    {
        private readonly IRequestBuilderFactory context;
        private readonly string token;

        internal AuthorizedBuilder(IRequestBuilderFactory context, string token)
        {
            this.context = context;
            this.token = token;
        }

        IRequestBuilder IRequestBuilderFactory.CreateRequestBuilder()
        {
            var builder = context.CreateRequestBuilder();
            builder.Headers["Authorization"] = $"Bearer {token}";
            return builder;
        }
    }
}
