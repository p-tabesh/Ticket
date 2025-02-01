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
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case CategoryException:
                    await context.Response.WriteAsync("category exception returned");
                    break;
                default:
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseBaseModel() { IsSuccess = false, StatusCode = 500, Message = "Somthing went wrong" /*exception.Message + " " + exception.InnerException*/ }));
                    break;
            }
        }
    }
}
