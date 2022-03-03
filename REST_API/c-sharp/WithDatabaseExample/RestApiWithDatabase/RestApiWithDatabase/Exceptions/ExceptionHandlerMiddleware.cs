using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestApiWithDatabase.Services;

namespace RestApiWithDatabase.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string info = context.Request.Path;
                
                //using var reader = new StreamReader(context.Response.Body);
                
                // reader.BaseStream.Seek(0, SeekOrigin.Begin); 
                // var body = await reader.ReadToEndAsync();
                //
                //
                // info += " "+ body;
                this.logger.Info(info);
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
                // StreamWriter file = new("log.txt", true);
                // 
                // file.WriteLine(error);
                string error = "\n\n"+exception.Message + "-" + exception.StackTrace;
                this.logger.Error(error);
            }

            ErrorDetail detail = new ErrorDetail() {Message = message, StatusCode = statusCode};
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(detail.ToString());

        }
    }
}