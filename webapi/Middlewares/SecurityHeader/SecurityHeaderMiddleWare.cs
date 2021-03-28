using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Middlewares.SecurityHeader
{
    public class SecurityHeaderMiddleWare:IMiddleWare
    {
        public RequestDelegate Next { get; init; }
        private readonly SecurityHeadersPolicy _policy;

        public SecurityHeaderMiddleWare(RequestDelegate next, SecurityHeadersPolicy policy)
        {
            Next = next;
            _policy = policy;
        }

        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;

            foreach (var headerValuePair in _policy.SetHeaders)  { headers[headerValuePair.Key] = headerValuePair.Value;  }

            foreach (var header in _policy.RemoveHeaders) { headers.Remove(header); }

            await Next(context);
        }
    }
}
