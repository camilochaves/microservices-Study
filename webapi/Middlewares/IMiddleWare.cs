using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.API.Middlewares
{
    public interface IMiddleWare
    {
        RequestDelegate Next { get; init; }
        public Task Invoke(HttpContext context);
    }
}
