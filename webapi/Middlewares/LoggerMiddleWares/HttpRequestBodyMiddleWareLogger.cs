using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Web.API.Middlewares.Logger
{
    public class HttpRequestBodyMiddleWareLogger : IMiddleWare
    {
        public RequestDelegate Next { get; init; }
        private readonly ILogger _logger;
        
        public HttpRequestBodyMiddleWareLogger(RequestDelegate next, 
            ILogger<HttpRequestBodyMiddleWareLogger> logger)
        {
            Next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();
            _logger.LogInformation($"Request {context.Request?.Method} : " +
                $"{context.Request?.Path.Value} \n {body}");
            await Next(context);
        }

    }
}
