using FluentDegiro.Abstractions.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Infrastructure
{
    internal class StreamApiMethod : ApiMethod, IApiMethod<Stream>
    {
        internal StreamApiMethod(Func<IRequestBuilder> builderFactory) : base(builderFactory)
        {
        }

        internal StreamApiMethod(IRequestBuilderFactory factory) : base(factory)
        {
        }

        internal StreamApiMethod(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }

        public async Task<Stream> QueryAsync(HttpClient client, CancellationToken cancellationToken = default)
        {
            // dont dispose response, otherwise the stream will not be readable
            var res = await CallAsyncInternal(client, cancellationToken).ConfigureAwait(false);
            return await res.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }
    }
}
