using ecommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ecommerce.SharedLibrary.Middleware;

public  class GlobalException(RequestDelegate next)
{
public async Task InvokeAsync(HttpContext httpContext)
    {
        var statusCode = httpContext.Response.StatusCode;

        string title;
        string message;
        try
        {
            switch (statusCode)
            {
                case StatusCodes.Status200OK:
                    title = "Success";
                    message = "Request completed successfully.";
                    break;

                case StatusCodes.Status201Created:
                    title = "Created";
                    message = "Resource created successfully.";
                    break;

                case StatusCodes.Status204NoContent:
                    title = "Success";
                    message = "Request completed successfully.";
                    break;

                case StatusCodes.Status400BadRequest:
                    title = "Bad Request";
                    message = "The request is invalid.";
                    break;

                case StatusCodes.Status401Unauthorized:
                    title = "Unauthorized";
                    message = "Authentication is required.";
                    break;

                case StatusCodes.Status403Forbidden:
                    title = "Forbidden";
                    message = "You do not have permission to access this resource.";
                    break;

                case StatusCodes.Status404NotFound:
                    title = "Not Found";
                    message = "The requested resource was not found.";
                    break;

                case StatusCodes.Status405MethodNotAllowed:
                    title = "Method Not Allowed";
                    message = "The requested HTTP method is not supported.";
                    break;

                case StatusCodes.Status408RequestTimeout:
                    title = "Request Timeout";
                    message = "The request timed out.";
                    break;

                case StatusCodes.Status409Conflict:
                    title = "Conflict";
                    message = "The request could not be completed because of a conflict.";
                    break;

                case StatusCodes.Status413PayloadTooLarge:
                    title = "Payload Too Large";
                    message = "The request payload exceeds the allowed size.";
                    break;

                case StatusCodes.Status415UnsupportedMediaType:
                    title = "Unsupported Media Type";
                    message = "The request content type is not supported.";
                    break;

                case StatusCodes.Status422UnprocessableEntity:
                    title = "Validation Error";
                    message = "One or more validation errors occurred.";
                    break;

                case StatusCodes.Status429TooManyRequests:
                    title = "Warning";
                    message = "Too many requests made.";
                    break;

                case StatusCodes.Status500InternalServerError:
                    title = "Internal Server Error";
                    message = "An unexpected error occurred.";
                    break;

                case StatusCodes.Status501NotImplemented:
                    title = "Not Implemented";
                    message = "The requested functionality is not implemented.";
                    break;

                case StatusCodes.Status502BadGateway:
                    title = "Bad Gateway";
                    message = "The server received an invalid response from an upstream server.";
                    break;

                case StatusCodes.Status503ServiceUnavailable:
                    title = "Service Unavailable";
                    message = "The service is temporarily unavailable.";
                    break;

                case StatusCodes.Status504GatewayTimeout:
                    title = "Gateway Timeout";
                    message = "The upstream server failed to respond in time.";
                    break;

                default:
                    title = "Error";
                    message = "An unexpected error occurred.";
                    break;
            }

            await ModifyHeader(httpContext, title, message, statusCode);
        }
        catch (Exception e) {
            LogException.LogExceptions(e);
        }
    }

    private async Task ModifyHeader(HttpContext httpContext, string title, string message, int statusCode)
    {
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails {
            Detail=message,
            Status = statusCode,
            Title=title,
        }),CancellationToken.None);
        return;
    }
}
