using System.Threading.Tasks;
using LoginForm.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace LoginForm.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var userActivityService = httpContext.RequestServices.GetService<IUserActivityService>();

            HttpRequest Request = httpContext.Request;
            string IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            string Body;
            using (var StreamReader = new System.IO.StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {
                Body = StreamReader.ReadToEnd();
            }

            var request = new CreateUserActivityRequest(IPAddress,
                                                         Request.Headers["User-Agent"],
                                                         Request.Cookies.ToString(),
                                                         Request.Path,
                                                         Body);

            CreateUserActivityResponse createUserActivityResponse = userActivityService.Create(request);

            
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
