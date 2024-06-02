using Iridium.Domain.Common;
using Iridium.Infrastructure.Utilities;
using Newtonsoft.Json;
using NLog;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Extensions;
using Iridium.Core.Enums;
using Iridium.Infrastructure.Services;

namespace Iridium.Web.Middlewares;

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
        var originalResponseBodyStream = context.Response.Body;
        var requestBodyContent = await ReadRequestBodyAsync(context);
        var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
        var queryString = context.Request.Path.Value + context.Request.QueryString.Value;

        var header = context.Request.Headers.Authorization;

        var requestStart = DateTime.UtcNow;
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                var responseBodyContent = await ReadResponseBodyAsync(memoryStream);
                var requestEnd = DateTime.UtcNow;
                
                // TODO: UserId will be add
                if (!responseBodyContent.IsNullOrEmpty() && !requestBodyContent.IsNullOrEmpty())
                {
                    if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                    {
                        Logger.Bilgi(requestBodyContent, responseBodyContent, remoteIpAddress,
                            LogType.ErrorHandlerMiddleware, "Standard", null, requestStart, requestEnd);
                    }
                }

                await memoryStream.CopyToAsync(originalResponseBodyStream);
            }
        }
        catch (Exception exception)
        {
            context.Response.Body = originalResponseBodyStream;
            await HandleExceptionAsync(context, exception, requestBodyContent, queryString, requestStart, remoteIpAddress);
        }
    }

    private async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; 
        return body;
    }

    private async Task<string> ReadResponseBodyAsync(MemoryStream memoryStream)
    {
        memoryStream.Position = 0;
        var reader = new StreamReader(memoryStream);
        var body = await reader.ReadToEndAsync();
        memoryStream.Position = 0; 
        return body;
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string requestBody, string queryString, DateTime start, string remoteIpAddress)
    {
        var responseEnd = DateTime.UtcNow;

        var errorCode = Guid.NewGuid().ToString();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)DetermineStatusCode(exception);

        var innerExceptionMessage = string.Empty;
        var exceptionMessage = string.Empty;
        
        if (exception.InnerException != null)
            innerExceptionMessage = exception.InnerException.Message;

        if (!exception.Message.IsNullOrEmpty())
            exceptionMessage = exception.Message;
        
        var outgoingModel = new
        {
            Succeeded = false,
            Message = $"An unexpected error occurred. (Code: {errorCode})"
        };

        var outgoing = System.Text.Json.JsonSerializer.Serialize(outgoingModel);
        
        var incoming = !string.IsNullOrEmpty(requestBody) ? requestBody : queryString;
        
        var exceptionLogModel = new
        {
            Succeeded = false,
            Message = $"An unexpected error occurred. (Code: {errorCode})",
            Exception = $"Exception : {exceptionMessage} ({innerExceptionMessage})",
        };
        var exceptionLog = System.Text.Json.JsonSerializer.Serialize(exceptionLogModel);
        
        if (context.Response.ContentType.Contains("application/json"))
        {
            Logger.Hata(incoming, exceptionLog, remoteIpAddress, LogType.ErrorHandlerMiddleware, "Error", errorCode,
                start, responseEnd);
        }

        await context.Response.WriteAsync(outgoing); 
    }

    private HttpStatusCode DetermineStatusCode(Exception exception)
    {
        return exception switch
        {
            KeyNotFoundException _ => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };
    }
}

