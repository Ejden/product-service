using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using product_service.Domain;

namespace product_service.Infrastructure.ExceptionHandlers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ProductNotFoundException cause)
            {
                await HandleExceptionAsync(httpContext, cause);
            }
            catch (ValidationException cause)
            {
                await HandleExceptionAsync(httpContext, cause);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, ProductNotFoundException cause)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.NotFound;

            return context.Response.WriteAsync(new ErrorDetails(cause.Message).ToJson());
        }
        
        private Task HandleExceptionAsync(HttpContext context, ValidationException cause)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.Conflict;

            return context.Response.WriteAsync(new ErrorDetails(cause.Message).ToJson());
        }
    }
}
