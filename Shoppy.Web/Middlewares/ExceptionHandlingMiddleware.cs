using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shoppy.Domain.Exceptions;
using Shoppy.Web.Models;

namespace Shoppy.Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext,
        IHostingEnvironment environment,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            return _next(httpContext);
        }
        catch (Exception e) when (e is BusinessException ex)
        {
            logger.LogInformation(ex.Message, ex);
            return WriteResponseAsync(
                httpContext.Response,
                HttpStatusCode.BadRequest,
                new ApiResult(false, ex.Message));
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex.Message, ex);
            var message = environment.IsProduction()
                ? "One or more error occurred" : ex.Message;
            return WriteResponseAsync(
                httpContext.Response,
                HttpStatusCode.InternalServerError,
                new ApiResult(false, message));
        }
    }

    private Task WriteResponseAsync(HttpResponse response,
        HttpStatusCode statusCode,
        ApiResult apiResult)
    {
        response.StatusCode = (int)statusCode;
        response.ContentType = "application/json";
        var settings = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };
        return response.WriteAsync(
            JsonSerializer.Serialize(apiResult, settings));
    }
}