using Domain.Validation;
using System.Text.Json;

namespace Triad.Middleware
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
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning(ex, ex.Message);

                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            int statusCode,
            string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    statusCode,
                    erro = message
                }));
        }
    }
}

//retira os Try e catch dos controllers
//e logger deixa o erro no console para indentificar dps
//Padronização de retorno de erro