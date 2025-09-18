using MSProducts.Presentation.Contracts;

namespace MSProducts.Presentation.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var traceId = context.TraceIdentifier;
                var response = ApiResponse<string>.ErrorResponse(
                    "An unexpected error occurred",
                    500,
                    new List<string> { ex.Message }
                );
                response.TraceId = traceId;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
