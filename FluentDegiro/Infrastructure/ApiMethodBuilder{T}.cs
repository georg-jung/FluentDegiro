using FluentDegiro.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Infrastructure
{
    internal class ApiMethodBuilder<TEntity> : ApiMethodBuilder, IApiMethod<TEntity>
    {
        internal ApiMethodBuilder(IRequestBuilderFactory context) : base(context)
        {
        }

        private ApiMethodBuilder(IRequestBuilderFactory context, ExpandoObject queryStringParameters, object body) : base(context, queryStringParameters, body)
        {
        }

        public async Task<TEntity> QueryAsync(HttpClient client, CancellationToken cancellationToken = default)
        {
            using var res = await SendAsync(client, cancellationToken).ConfigureAwait(false);
            return await ApiMethod<TEntity>.QueryAsync(res).ConfigureAwait(false);
        }

        internal static new ApiMethodBuilder<TEntity> Create(IRequestBuilderFactory context, ExpandoObject queryStringParameters = null, object body = null)
            => Create(context, HttpMethod.Get, queryStringParameters, body);

        internal static new ApiMethodBuilder<TEntity> Create(
            IRequestBuilderFactory context,
            HttpMethod method,
            ExpandoObject queryStringParameters = null,
            object body = null)
        {
            var meth = new ApiMethodBuilder<TEntity>(context, queryStringParameters, body)
            {
                Method = method
            };
            return meth;
        }
    }
}
