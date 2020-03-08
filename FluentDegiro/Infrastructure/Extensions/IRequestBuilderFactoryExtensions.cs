using FluentDegiro.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;

namespace FluentDegiro.Infrastructure.Extensions
{
    internal static class IRequestBuilderFactoryExtensions
    {
        public static ApiMethodBuilder<TEntity> CreateApiMethod<TEntity>(
            this IRequestBuilderFactory factory,
            HttpMethod method,
            ExpandoObject queryStringParameters = null,
            object body = null)
            => ApiMethodBuilder<TEntity>.Create(factory, method, queryStringParameters, body);

        public static ApiMethodBuilder<TEntity> CreateApiMethod<TEntity>(
            this IRequestBuilderFactory factory,
            ExpandoObject queryStringParameters = null,
            object body = null)
            => ApiMethodBuilder<TEntity>.Create(factory, queryStringParameters, body);

        public static ApiMethodBuilder CreateApiMethod(
            this IRequestBuilderFactory factory,
            HttpMethod method,
            ExpandoObject queryStringParameters = null,
            object body = null)
            => ApiMethodBuilder.Create(factory, method, queryStringParameters, body);

        public static ApiMethodBuilder CreateApiMethod(
            this IRequestBuilderFactory factory,
            ExpandoObject queryStringParameters = null,
            object body = null)
            => ApiMethodBuilder.Create(factory, queryStringParameters, body);
    }
}
