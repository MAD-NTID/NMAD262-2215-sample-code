using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RestApiWithDatabase.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //by default we assume a system error
            int statusCode = 500;
            string message = "An internal error occurred while processing your request";
            

            //error is an instance of user exception error so we want to show that error to the user
            if (exception is IUserErrorException)
            {
                UserErrorException err = (UserErrorException) exception;
                statusCode = err.getStatusCode();
                message = err.getMessage();
            }
            
            
            //system error logs
            if (statusCode == 500)
            {
                //log this
                StreamWriter file = new("log.txt", true);
                string error = exception.Message + "\n" + exception.StackTrace;
                file.WriteLine(error);
            }

            ErrorDetail detail = new ErrorDetail() {Message = message, StatusCode = statusCode};
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(detail.ToString());

        }
    }
}