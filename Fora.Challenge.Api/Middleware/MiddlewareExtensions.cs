namespace Fora.Challenge.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        /// <summary>Uses the custom exception handler.</summary>
        /// <param name="builder">The builder.</param>
        /// <returns>An application builder.</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
