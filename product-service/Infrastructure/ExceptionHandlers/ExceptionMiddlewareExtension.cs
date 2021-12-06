using Microsoft.AspNetCore.Builder;

namespace product_service.Infrastructure.ExceptionHandlers
{
    public static class ExceptionMiddlewareExtension
    {
        
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}