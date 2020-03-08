using FluentDegiro.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Infrastructure
{
    /// <summary>
    /// Abstract base for an api method that provides sending functionality to it's children, which need to provide an IRequestBuilder.
    /// </summary>
    internal abstract class ApiMethodBase : IApiMethod
    {
        protected abstract IRequestBuilder CreateRequestBuilder();

        public static async Task<HttpResponseMessage> SendAsync(IRequestBuilder requestBuilder, HttpClient client, CancellationToken cancellationToken = default)
        {
            if (requestBuilder == null)
                throw new ArgumentNullException(nameof(requestBuilder));
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using var req = requestBuilder.Build();
            return await client.SendAsync(req, cancellationToken).ConfigureAwait(false);
        }

        public Task<HttpResponseMessage> SendAsync(HttpClient client, CancellationToken cancellationToken = default)
        {
            var builder = CreateRequestBuilder();
            return SendAsync(builder, client, cancellationToken);
        }

        protected static async Task ThrowOnFailureAsync(HttpResponseMessage res)
        {
            if (res.IsSuccessStatusCode)
                return;
            var contentStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            // some of the callers will dispose the res just if this method does not throw
            res.Dispose();
            throw new NoSuccessStatusCodeException($"The API returned HTTP status {(int)res.StatusCode}.", res.StatusCode, contentStr);
        }

        protected async Task<HttpResponseMessage> CallAsyncInternal(HttpClient client, CancellationToken cancellationToken = default)
        {
            var res = await SendAsync(client, cancellationToken).ConfigureAwait(false);
            await ThrowOnFailureAsync(res).ConfigureAwait(false);
            // dont dispose as it is returned
            return res;
        }

        public async Task ExecuteAsync(HttpClient client, CancellationToken cancellationToken = default)
        {
            // throw away the response, but we need to dispose it
            using var res = await CallAsyncInternal(client, cancellationToken).ConfigureAwait(false);
        }
    }
}
