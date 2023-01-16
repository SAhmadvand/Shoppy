using Microsoft.AspNetCore.Builder;
using Shoppy.Web.Middlewares;

namespace Shoppy.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseShoppyExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}