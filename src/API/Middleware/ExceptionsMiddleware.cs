using System.Net;
using System.Text.Json;
using FluentValidation;

namespace API.Middleware
{
    public static class ExceptionsMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (ValidationException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    var response = new
                    {
                        errors = ex.Errors.Select(e => e.ErrorMessage)
                    };
                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(jsonResponse);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var response = new
                    {
                        error = ex.InnerException is not null
                            ? "An unexpected error occurred. " + ex.InnerException.Message
                            : "An unexpected error occurred. " + ex.Message,
                    };
                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(jsonResponse);
                }
            });

            return app;
        }
    }
}
