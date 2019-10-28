using Newtonsoft.Json;
using System;
using System.Net;

namespace App.Api.Exceptions
{
    public class ErrorDetail : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public override string Message { get; }

        public ErrorDetail(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
