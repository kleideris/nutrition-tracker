using NutritionTracker.Api.Exceptions;
using System.Net;
using System.Text.Json;

namespace NutritionTracker.Api.Core.Helpers
{
    // You can use this error handler if GlobalExceptionHandler doesnt work.
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Delegates exceptions to the GlobalExceptionHandler
        //        var handler = context.RequestServices.GetRequiredService<IExceptionHandler>();
        //        var handled = await handler.TryHandleAsync(context, ex, CancellationToken.None);

        //        if()

        //        if (!handled)
        //        {
        //            // Fallback logic if GlobalExceptionHandler doesn't handle it
        //            context.Response.StatusCode = 500;
        //            await context.Response.WriteAsync("Unhandled error occurred.");
        //        }
        //    }
        //}

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                // 🔍 Check for silent authorization failures
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden ||
                    context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    var username = context.User.Identity?.Name ?? "Anonymous";
                    var path = context.Request.Path;
                    var method = context.Request.Method;

                    var logger = context.RequestServices.GetRequiredService<ILogger<ErrorHandlerMiddleware>>();
                    logger.LogWarning("Access denied: {StatusCode} | User: {User} | Path: {Path} | Method: {Method}",
                        context.Response.StatusCode, username, path, method);

                    // 🔁 Replace response with custom JSON
                    context.Response.ContentType = "application/json";
                    context.Response.Body.SetLength(0); // Clear existing response

                    var errorJson = JsonSerializer.Serialize(new
                    {
                        code = "AccessDenied",
                        message = "You do not have permission to access this resource.",
                        statusCode = context.Response.StatusCode
                    });

                    await context.Response.WriteAsync(errorJson);
                }
                else
                {
                    // 🔄 Copy original response back
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    InvalidRegistrationException or EntityAlreadyExistsException => (int)HttpStatusCode.BadRequest,
                    EntityNotAuthorizedException => (int)HttpStatusCode.Unauthorized,
                    EntityForbiddenException => (int)HttpStatusCode.Forbidden,
                    EntityNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var result = JsonSerializer.Serialize(new { message = ex.Message });
                await response.WriteAsync(result);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception exception)
        //    {
        //        var response = context.Response;
        //        response.ContentType = "application/json";

        //        response.StatusCode = exception switch
        //        {
        //            InvalidRegistrationException or
        //            EntityAlreadyExistsException => (int)HttpStatusCode.BadRequest,   // 400

        //            EntityNotAuthorizedException => (int)HttpStatusCode.Unauthorized,    // 401
        //            EntityForbiddenException => (int)HttpStatusCode.Forbidden,               // 403
        //            EntityNotFoundException => (int)HttpStatusCode.NotFound,             // 404
        //            _ => (int)HttpStatusCode.InternalServerError
        //        };

        //        var result = JsonSerializer.Serialize(new { message = exception?.Message });
        //        await response.WriteAsync(result);
        //    }
        //}
    }
}
