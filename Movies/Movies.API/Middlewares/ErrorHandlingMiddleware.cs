using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace Movies.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            var message = exception.Message;

            switch (exception)
            {
                case ValidationException ex:
                    code = HttpStatusCode.BadRequest;
                    _logger.LogWarning(exception, $"Bad Request: {ex.Message} - {context.Request.Method} {context.Request.Path + context.Request.QueryString}.");
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    message = "Something went wrong during request handling";
                    _logger.LogError(exception, $"Error: {exception.Message}.");
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(message);
        }
    }
}
