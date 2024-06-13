using System.Net;
using System.Text.Json;
using ApplicationAPI.Errors;

namespace ApplicationAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env) 
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                int statusCode = (int) HttpStatusCode.InternalServerError;

                var result = _env.IsDevelopment()
                    ? new ApiException(statusCode, ex.Message, ex.StackTrace.ToString())
                    : new ApiException(statusCode, ex.Message);

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(result, jsonOptions);

                await context.Response.WriteAsJsonAsync(json);

            }
        }
    }
}