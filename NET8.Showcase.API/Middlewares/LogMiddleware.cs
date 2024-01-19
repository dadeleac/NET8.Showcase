using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.IdentityModel.Abstractions;

namespace NET8.Showcase.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TelemetryClient _telemetryClient;


        public LogMiddleware(RequestDelegate next, TelemetryClient telemetryClient)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;
            var requestMethod = context.Request.Method;
            await _next(context);

            var responseStatusCode = context.Response.StatusCode;
            SendTelemetry($"{requestMethod} {requestPath}", responseStatusCode, requestPath, requestMethod);


        }

        private void SendTelemetry(string request, int statusCode, string url, string method)
        {
            var telemetry = new TraceTelemetry("Custom Trace"); 

            telemetry.Properties.Add("Request", request);
            telemetry.Properties.Add("StatusCode", statusCode.ToString());
            telemetry.Properties.Add("Url", url);
            telemetry.Properties.Add("Method", method);

            _telemetryClient.TrackTrace(telemetry);
            _telemetryClient.Flush();
        }


    }
}
