using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.Models;
using Newtonsoft.Json;
using System.Net.Mime;

namespace CoffeeShopAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong on {context.Request.Path}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var errorDetails = new ErrorDetails
            {
                ErrorMessage = ex.Message
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case BadRequestException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    break;
                default:
                    errorDetails.ErrorType = "Failure";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            return context.Response.WriteAsync(response);
        }
    }
}
