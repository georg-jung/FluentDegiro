using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Infrastructure
{
    internal class ApiMethod<TEntity> : ApiMethod, IApiMethod<TEntity>
    {
        internal ApiMethod(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }

        internal ApiMethod(IRequestBuilderFactory factory) : base(factory)
        {
        }

        internal ApiMethod(Func<IRequestBuilder> builderFactory) : base(builderFactory)
        {
        }

        public static async Task<TEntity> QueryAsync(HttpResponseMessage res, CancellationToken cancellationToken = default)
        {
            await ThrowOnFailureAsync(res).ConfigureAwait(false);
            using var stream = await res.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TEntity>(stream, Degiro.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        public async Task<TEntity> QueryAsync(HttpClient client, CancellationToken cancellationToken = default)
        {
            using var res = await CallAsyncInternal(client, cancellationToken).ConfigureAwait(false);
            using var stream = await res.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<TEntity>(stream, Degiro.JsonSerializerOptions, cancellationToken: cancellationToken);
        }
    }
}
