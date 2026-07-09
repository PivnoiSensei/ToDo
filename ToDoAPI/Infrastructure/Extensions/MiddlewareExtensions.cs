using ToDoAPI.Infrastructure.Middleware;

namespace ToDoAPI.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
       public static IApplicationBuilder UseExceptionMiddleware(
           this IApplicationBuilder app)
       {
           app.UseMiddleware<ExceptionMiddleware>();

           return app;
       }
    }
}
