using Microsoft.AspNetCore.Http;

namespace ecommerce.SharedLibrary.Middleware;

public class ListenToOnlyAPIGateway(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext )
    {
        var signedHeader = httpContext.Request.Headers["Api-Gateway"];
        if(signedHeader.FirstOrDefault() is null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await httpContext.Response.WriteAsync("Sorry, Service is UnAvailable.");
            return;
        }
        else
        {
            await next(httpContext);
        }
    }
}
