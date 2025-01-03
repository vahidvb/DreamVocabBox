using Common.Extensions;
using Newtonsoft.Json;
using System;

namespace Common.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiResult(ApiResultStatusCode statusCode = ApiResultStatusCode.Success, string message = null)
        {
            IsSuccess = statusCode.GetCustomStatus();
            StatusCode = (int)statusCode;
            Message = message ?? statusCode.GetCustomDisplayName();
        }
    }

    public class ApiResult<TData> : ApiResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(TData data, ApiResultStatusCode statusCode = ApiResultStatusCode.Success, string message = null)
            : base(statusCode, message)
        {
            Data = data;
        }
    }
}