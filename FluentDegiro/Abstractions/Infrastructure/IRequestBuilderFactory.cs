using System;
using System.Collections.Generic;
using System.Text;

namespace FluentDegiro.Abstractions.Infrastructure
{
    internal interface IRequestBuilderFactory
    {
        IRequestBuilder CreateRequestBuilder();
    }
}
