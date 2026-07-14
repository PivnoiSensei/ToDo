using Microsoft.AspNetCore.Http;
using ToDoAPI.Infrastructure.Exceptions;

namespace ToDoAPI.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
     HttpContext context,
     Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiError response;
            int statusCode;

            switch (exception)
            {
                case RequestValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;

                    response = new ApiError(
                        statusCode,
                        validationException.Message,
                        context.TraceIdentifier,
                        validationException.Errors);

                    break;

                case ConflictException:
                    statusCode = StatusCodes.Status409Conflict;

                    response = new ApiError(
                        statusCode,
                        exception.Message,
                        context.TraceIdentifier);

                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;

                    response = new ApiError(
                        statusCode,
                        exception.Message,
                        context.TraceIdentifier);

                    break;

                case UnauthorizedException:
                    statusCode = StatusCodes.Status401Unauthorized;

                    response = new ApiError(
                        statusCode,
                        exception.Message,
                        context.TraceIdentifier);

                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;

                    response = new ApiError(
                        statusCode,
                        "An unexpected error occurred.",
                        context.TraceIdentifier);

                    break;
            }

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
