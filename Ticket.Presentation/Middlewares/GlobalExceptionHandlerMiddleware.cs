using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Ticket.Application.Models;
using Ticket.Domain.Exceptions;

namespace Ticket.Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
                return;

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var responseMessage = new ResponseBaseModel() { IsSuccess = false };
            switch (exception)
            {
                case CategoryException:
                    responseMessage.Message = exception.Message;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(responseMessage));
                    break;
                case UnauthorizedAccessException:
                    responseMessage.Message = "UnAuthorization";
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(responseMessage));
                    break;
                case Exception:
                    responseMessage.Message = exception.Message;
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(responseMessage));
                    break;
                default:
                    responseMessage.Message = exception.Message;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(responseMessage));
                    break;
            }
        }

    }
}

