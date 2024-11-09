using Microsoft.Data.SqlClient;
using System.Net;

namespace prof_edna_teles_shop_api.Middleware;

public class ExceptionMiddleware
{
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;

        switch (exception)
        {
            case SqlException:
                statusCode = HttpStatusCode.InternalServerError;
                break;
            case TimeoutException:
                statusCode = HttpStatusCode.RequestTimeout;
                break;
            case InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            case IndexOutOfRangeException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(new
        {
            context.Response.StatusCode,
            exception.Message
        });
    }

}
