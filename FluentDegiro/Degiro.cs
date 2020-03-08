using FluentDegiro.Abstractions.Builders;
using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Builders;
using FluentDegiro.Constants;
using FluentDegiro.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FluentDegiro
{
    public static class Degiro
    {
        public static Uri EndpointUrl { get; set; } = new Uri(ApiConstants.BaseUrl);
        internal static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public static IAuthorizedSyntax WithToken(string token)
        {
            return new AuthorizedBuilder(CreateContext(), token);
        }

        private static IRequestBuilderFactory CreateContext() => new EndpointFactory();

        private class EndpointFactory : IRequestBuilderFactory
        {
            public IRequestBuilder CreateRequestBuilder()
            {
                var builder = new RequestBuilder
                {
                    BaseUrl = EndpointUrl
                };
                return builder;
            }
        }
    }
}
