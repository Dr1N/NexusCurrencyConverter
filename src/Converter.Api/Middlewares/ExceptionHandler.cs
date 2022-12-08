using System.Net;
using System.Text.Json;
using Converter.Domain.Exceptions;

namespace Converter.Api.Middlewares;

/// <summary>
/// Global Exception handler. Translate exception to error response.
/// Logs exceptions
/// </summary>
public class ExceptionHandler
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandler> logger;
    
    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        this.next = next;
        this.logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        var errors = new List<string>();

        if (exception is ApplicationException or DomainException)
        {
            code = HttpStatusCode.BadRequest;
            errors.AddRange(GetMessagesFromException(exception));
            logger.LogWarning( exception,$"Application error");
        }
        else
        {
            code = HttpStatusCode.InternalServerError;
            errors.Add("Internal server error");
            logger.LogError(exception,"Server error");
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var response = new { errors };
        var json = JsonSerializer.Serialize(response);
        
        return context.Response.WriteAsync(json);
    }

    private static IEnumerable<string> GetMessagesFromException(Exception exception)
    {
        var errors = new List<string> { exception.Message };
        while (exception.InnerException != null)
        {
            errors.Add(exception.InnerException.Message);
            exception = exception.InnerException;
        }

        return errors.ToArray();
    }
}
