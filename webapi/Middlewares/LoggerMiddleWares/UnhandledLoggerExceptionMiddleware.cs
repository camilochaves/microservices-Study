using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Middlewares.Logger
{
    public class UnhandledLoggerExceptionMiddleware : IMiddleWare
    {
        public RequestDelegate Next { get; init; }
        private ILogger _logger;

        public UnhandledLoggerExceptionMiddleware(ILogger<UnhandledLoggerExceptionMiddleware> log, RequestDelegate next)
        {
            _logger = log;
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Request {context.Request?.Method} : " +
                    $"{context.Request?.Path.Value} failed");
            }
            
        }
    }
}
