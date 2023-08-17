using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;


namespace Helper.Response
{
    public class ServiceResponse<TResponsType>
    {
        public TResponsType? Data { get; set; }

        public bool Result { get; set; }

        public string? Message { get; set; }
    }

    public static class ServiceResponseFactory
    {
        public static ServiceResponse<TResponsType> Success<TResponsType>(TResponsType? responsType)
        {
            return new ServiceResponse<TResponsType>()
            {
                Data = responsType,
                Result = true,
                Message = null
            };
        }


        public static ServiceResponse<string> Failure(string message)
        {
            return new ServiceResponse<string>()
            {
                Data = null,
                Result = false,
                Message = message
            };
        }
    }
}
