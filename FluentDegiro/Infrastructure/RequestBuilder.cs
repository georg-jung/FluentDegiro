﻿using FluentDegiro.Abstractions.Infrastructure;
using FluentDegiro.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FluentDegiro.Infrastructure
{
    internal class RequestBuilder : IRequestBuilder
    {
        public Uri BaseUrl { get; set; }
        public List<string> UrlSegments { get; } = new List<string>();
        public ExpandoObject QueryStringParameters { get; } = new ExpandoObject();
        public object Body { get; set; }
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public HttpMethod Method { get; set; } = HttpMethod.Get;

        public HttpRequestMessage Build()
        {
            var uri = BaseUrl.Append(UrlSegments);
            var getParams = QueryStringParameters.ToQueryString();
            if (!string.IsNullOrEmpty(getParams))
                uri = uri.Append($"?{getParams}");
            var req = new HttpRequestMessage(Method, uri);
            foreach ((var header, var value) in Headers)
            {
                req.Headers.Add(header, value);
            }
            var jsonBody = Body == null ? null : JsonSerializer.Serialize(Body, Degiro.JsonSerializerOptions);
            if (!string.IsNullOrEmpty(jsonBody) && !jsonBody.Equals("{}", StringComparison.OrdinalIgnoreCase))
                req.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            return req;
        }
    }
}
