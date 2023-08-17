using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Helper.Response
{
    //public class CustomServiceResponse: IActionResult
    //{

    //    private readonly int _statusCode;

    //    private readonly ServiceResponse<string> _serviceResponse;

    //    public CustomServiceResponse(string value,Boolean result, int statusCode)
    //    {
    //        _serviceResponse = new ServiceResponse<string>(null,false, value);
    //        _statusCode = statusCode;
    //    }
    //    public async Task ExecuteResultAsync(ActionContext context)
    //    {
    //        var objectResult = new ObjectResult(_serviceResponse)
    //        {
    //            StatusCode = _statusCode
    //        };

    //        await objectResult.ExecuteResultAsync(context);
    //    }
    //}

    //public class CustomOkServiceResponse<TResponsType> : IActionResult
    //{
    //    private readonly ServiceResponse<TResponsType> _serviceResponse;

    //    public CustomOkServiceResponse(TResponsType value)
    //    {
    //        _serviceResponse = new ServiceResponse<TResponsType>(value,true, "Success");
    //    }
    //    public async Task ExecuteResultAsync(ActionContext context)
    //    {
    //        var objectResult = new ObjectResult(_serviceResponse)
    //        {
    //            StatusCode = (int)HttpStatusCode.OK
    //        };

    //        await objectResult.ExecuteResultAsync(context);
    //    }
    //}
}