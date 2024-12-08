using Fora.Challenge.Application.Exceptions;
using Fora.Challenge.Application.Features.Responses;
using System.Net;

namespace Fora.Challenge.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.</summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>Invokes the specified context.</summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        /// <summary>Converts the exception.</summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A task.</returns>
        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            LogLevel logLevel = LogLevel.Information;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    logLevel = LogLevel.Warning;
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    logLevel = LogLevel.Warning;
                    break;
                case Exception:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    logLevel = LogLevel.Error;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            var errorResult = new ErrorResponse(exception.Message, exception.HResult);

            _logger.Log(logLevel, exception.Message);

            return context.Response.WriteAsJsonAsync(errorResult);
        }
    }
}
