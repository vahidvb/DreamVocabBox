using Common;
using Common.Api;
using Common.Extensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

namespace WebFramework.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            var apiStatusCode = ApiResultStatusCode.UnHandledError;
            Exception ex = null;

            try
            {
                await next(context);
            }
            catch (AppException exception)
            {
                ex = exception;
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception._ApiStatusCodes;

                message = apiStatusCode.GetCustomDisplayName() ?? "An error has occurred";
                await WriteToResponseAsync(exception);
            }
            catch (SecurityTokenExpiredException exception)
            {
                ex = exception;
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                ex = exception;
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (AggregateException exception)
            {
                foreach (var item in exception.InnerExceptions)
                {
                    if (item.GetType() == typeof(AppException))
                    {
                        ex = exception;
                        httpStatusCode = ((AppException)item).HttpStatusCode;
                        apiStatusCode = ((AppException)item)._ApiStatusCodes;
                        message = apiStatusCode.ToDisplay();

                        await WriteToResponseAsync(exception);
                    }
                }
            }
            catch (Exception exception)
            {
                await WriteToResponseAsync(exception);
            }

            async Task WriteToResponseAsync(Exception exception)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new ApiResult(apiStatusCode, message);
                var json = JsonConvert.SerializeObject(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }

            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ApiResultStatusCode.UnAuthorized;
            }
        }
    }
}