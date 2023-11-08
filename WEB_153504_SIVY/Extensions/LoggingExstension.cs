using WEB_153504_SIVY.Logging;

namespace WEB_153504_SIVY.Extensions
{
    public static class LoggingExstension
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
