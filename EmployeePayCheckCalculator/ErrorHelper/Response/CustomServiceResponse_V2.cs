using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Helper.Response
{
    public static class CustomServiceResponse_V2
    {
        public static IActionResult Success<TResponsType>(TResponsType value)
        {
            var objectResult = new ObjectResult(ServiceResponseFactory.Success(value))
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return objectResult;
        }

        public static IActionResult Failure(string value, int statusCode)
        {
            var objectResult = new ObjectResult(ServiceResponseFactory.Failure(value))
            {
                StatusCode = statusCode
            };

            return objectResult;
        }

        public static IActionResult Failure(Exception ex)
        {
            var objectResult = new ObjectResult(ServiceResponseFactory.Failure("Unable to process the request"))
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            //Log the actual error message for troubleshooting. It is not a good idea to show the actual error to the end user. 
            //logger.LogError($"Inner Exception: {ex.InnerException} Stack Trace:{ex.StackTrace}");

            return objectResult;
        }
    }
}
