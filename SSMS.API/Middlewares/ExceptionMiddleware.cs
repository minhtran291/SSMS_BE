using FluentValidation;
using SSMS.Application.Exceptions;

namespace SSMS.API.Middlewares
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
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex, "An exception occurred after the response had already started.");
                    throw;
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                _logger.LogWarning(
                    validationException,
                    "Validation failed. TraceId: {TraceId}",
                    context.TraceIdentifier);

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    statusCode = StatusCodes.Status400BadRequest,
                    message = "Validation Failed",
                    errors,
                    traceId = context.TraceIdentifier
                });

                return;
            }

            var statusCode = GetStatusCode(exception);

            if (exception is BaseException)
            {
                _logger.LogWarning(
                exception,
                "Unhandled exception. TraceId: {TraceId}, StatusCode: {StatusCode}",
                context.TraceIdentifier,
                statusCode);
            }
            else
            {
                _logger.LogError(
                exception,
                "Unhandled exception. TraceId: {TraceId}, StatusCode: {StatusCode}",
                context.TraceIdentifier,
                statusCode);
            }

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                statusCode,
                message = statusCode == StatusCodes.Status500InternalServerError
                    ? "Internal Server Error"
                    : exception.Message,
                traceId = context.TraceIdentifier,
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                BaseException baseException => baseException.StatusCode,

                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
