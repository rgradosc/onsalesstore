using OnSalesStore.ECommerce.Application.UseCases.Common.Exceptions;
using OnSalesStore.ECommerce.Transversal.Common;
using System.Net;
using System.Text.Json;

namespace OnSalesStore.ECommerce.Services.WebAPI.Modules.GlobalException
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body,
                    new Response<object> { Message = "Validations errors", Errors = ex.Errors });

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logger.LogError($"Exception Details: {message}");
                var response = new Response<Object>()
                {
                    Message = message,
                };
                await JsonSerializer.SerializeAsync(context.Response.Body, response);
            }
        }
    }
}
