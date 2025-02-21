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
                if (context.Response.Headers["content-length"] == 0 || context.Response.Headers.Count == 0)
                {
                    var responseModel = new ResponseBaseModel() { Message = "success" };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = 500;
            var responseMessage = new ResponseBaseModel() { IsSuccess = false, StatusCode = 500 };
            switch (exception)
            {
                case CategoryException:
                    responseMessage.Message = exception.Message;
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
