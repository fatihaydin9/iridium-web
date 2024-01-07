using Iridium.Domain.Common;
using Iridium.Domain.Enums;
using Iridium.Infrastructure.Utilities;
using Newtonsoft.Json;
using NLog;
using System.Net;

namespace Iridium.Web.Middlewares
{
    // TODO : Log every request
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private Logger Logger => LogManager.GetCurrentClassLogger();
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            DateTime responseStart = DateTime.Now;
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;

                //Query String
                var query = context.Request.Path.Value + context.Request.QueryString.Value;

                //Request Body JSON
                var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                if (!string.IsNullOrEmpty(requestBody))
                {
                    var tempBody = JsonConvert.DeserializeObject(requestBody);
                    requestBody = JsonConvert.SerializeObject(tempBody);
                }

                var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
                response.ContentType = "application/json";
                var responseModel = new ServiceResult<string>() { Succeeded = false, Message = error.Message };

                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var outgoing = System.Text.Json.JsonSerializer.Serialize(responseModel);

                DateTime responseEnd = DateTime.Now;
                var incoming = !string.IsNullOrEmpty(requestBody) ? requestBody : query;

                Logger.Hata(incoming, outgoing, remoteIpAddress, LogType.ErrorHandlerMiddleware, "Error", null, responseStart, responseEnd);

                await response.WriteAsync(outgoing);
            }
        }
    }
}
