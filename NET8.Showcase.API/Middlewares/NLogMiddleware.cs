using NLog;

namespace NET8.Showcase.API.Middlewares
{
    public class NLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<NLogMiddleware> _logger;
        //private static readonly NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
        private NLog.Logger _nlogLogger;

        public NLogMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _nlogLogger = LogManager.GetCurrentClassLogger();

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _nlogLogger.Trace($"NLOG Request {context.Request?.Method}: {context.Request?.Path.Value}");

                await _next(context);

                _nlogLogger.Trace($"NLOG StatusCode Response: {context.Response?.StatusCode}");
            }
            catch (Exception ex)
            {
                _nlogLogger.Error(ex, "NLOG Unhandled exception occurred.");
                throw;
            }
        }
    }
}
