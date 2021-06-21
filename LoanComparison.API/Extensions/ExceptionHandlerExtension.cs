using LoanComparison.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LoanComparison.API.Extensions
{
    public static class ExceptionHandlerExtension
    {
        /// <summary>
        /// Configure custom exception handler to log exception and return internal server error
        /// </summary>
        /// <param name="loggerFactory">logger factory instance</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    //set the default response status code to internal server error
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var responseMessage = "Internal Server Error";
                    //get the exception details
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        if (exceptionHandlerFeature.Error is LoanComparisonValidationException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                            responseMessage = "Validation Error";
                        }
                        //get the logger instance
                        var logger = loggerFactory.CreateLogger("loanComparisonLogger");
                        //create log entry with exception details
                        logger.LogError(exceptionHandlerFeature.Error, "An Error occurred");
                    }
                   
                    //write response with error message
                    await context.Response.WriteAsync(responseMessage);
                });
            });
        }
    }
}
