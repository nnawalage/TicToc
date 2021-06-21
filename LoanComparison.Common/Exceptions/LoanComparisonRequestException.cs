using System;
using System.Net;

namespace LoanComparison.Common.Exceptions
{
    public class LoanComparisonRequestException : Exception
    {
        public LoanComparisonRequestException(HttpStatusCode statusCode) : base(GetMessage(statusCode))
        {
        }

        /// <summary>
        /// Get relevant exception message based on status code
        /// </summary>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <returns>string message</returns>
        private static string GetMessage(HttpStatusCode statusCode)
        {
            string message;
            switch (statusCode)
            {
                //401 and 403-Unauthorized
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    message = "Unauthorized";
                    break;
                //422 – Validation error
                case HttpStatusCode.UnprocessableEntity:
                    message = "Validation error";
                    break;
                //500 – Unexpected error
                case HttpStatusCode.InternalServerError:
                default:
                    message = "Unexpected error";
                    break;
            }
            return message;
        }

    }
}
