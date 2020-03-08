using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;

namespace FluentDegiro.Abstractions.Infrastructure
{
    internal interface IRequestBuilder
    {
        Uri BaseUrl { get; set; }
        object Body { get; set; }
        Dictionary<string, string> Headers { get; }
        HttpMethod Method { get; set; }
        ExpandoObject QueryStringParameters { get; }
        List<string> UrlSegments { get; }

        HttpRequestMessage Build();
    }
}