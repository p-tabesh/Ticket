using System.Net;
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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var responseMessage = new ResponseBaseModel() { IsSuccess = false, Message = exception.Message };

            if (exception.GetType() == typeof(BusinessException))
            {
                var customException = (BusinessException)exception;
                switch (customException.ErrorType)
                {
                    case ErrorType.NotFound:
                        context.Response.StatusCode = 404;
                        break;
                    case ErrorType.AddError:
                        context.Response.StatusCode = 400;
                        break;
                    case ErrorType.EditError:
                        context.Response.StatusCode = 400;
                        break;
                    case ErrorType.RemoveError:
                        context.Response.StatusCode = 400;
                        break;
                    case ErrorType.ValidationError:
                        context.Response.StatusCode = 400;
                        break;
                    default:
                        context.Response.StatusCode = 500;
                        break;
                }
                return;
            }
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseMessage));
            return;
        }
    }
}

