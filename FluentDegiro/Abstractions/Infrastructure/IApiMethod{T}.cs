using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentDegiro.Abstractions.Infrastructure
{
    public interface IApiMethod<TPayload> : IApiMethod
    {
        Task<TPayload> QueryAsync(HttpClient client, CancellationToken cancellationToken = default);
    }
}
