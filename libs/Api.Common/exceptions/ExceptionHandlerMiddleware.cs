using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Common.exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;

            var response = new ErrorResponse();
            
            if(exception is ErrorResponseException err)
            {
                context.Response.StatusCode = err.StatusCode;
                response = new ErrorResponse{
                    StatusCode=err.StatusCode,
                    Message=err.Description,
                    Errors=err.Errors
                };
            }else{
                response = new ErrorResponse
                {
                    StatusCode=statusCode,
                    Message = exception.Message
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

            if(exception is not ErrorResponseException)
                throw exception;
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}