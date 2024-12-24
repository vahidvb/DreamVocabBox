using Common.Api;
using System;
using System.Net;

namespace Common
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiResultStatusCode _ApiStatusCodes;
        public object AdditionalData { get; set; }

        public AppException(ApiResultStatusCode ApiStatusCodes, string message = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK, Exception exception = null, object additionalData = null)
            : base(message, exception)
        {
            _ApiStatusCodes = ApiStatusCodes;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}