using JLokaTestMiddleware.middleware;

namespace JLokaTestMiddleware.middleware
{
    public class CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault();
            if (correlationId == null)
            {
                correlationId = Guid.NewGuid().ToString();
            }

            context.Request.Headers.TryAdd(CorrelationIdHeaderName, correlationId);
            //log the data
            logger.LogInformation($"Request Path: {context.Request.Path}, with correlation id: {correlationId}");
            context.Response.Headers.TryAdd(CorrelationIdHeaderName, correlationId);
            await next(context);
        }
    }
}

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}