using Microsoft.AspNetCore.Builder;
using Web.API.Middlewares.Logger;
using Web.API.Middlewares.SecurityHeader;

namespace Web.API.Middlewares
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseMiddleware<T>(this IApplicationBuilder app) => app.UseMiddleware<T>();

        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
        {
            SecurityHeadersPolicy policy = builder.Build();
            return app.UseMiddleware<SecurityHeaderMiddleWare>(policy);
        }

        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder) =>
            builder.UseMiddleware<HttpRequestBodyMiddleWareLogger>();
        
    }
}
