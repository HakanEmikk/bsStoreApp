using Entity.ErrorModel;
using Entity.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Service.Contracts;
using System.Net;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app,
            ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                   
                    context.Response.ContentType = "application/json";

                    var contexeFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contexeFeature is not null)
                    {
                        context.Response.StatusCode = contexeFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError,
                        };

                        logger.LogError($"Something went wrong: {contexeFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails() {
                        StatusCode=context.Response.StatusCode,
                        Message=contexeFeature.Error.Message
                        }.ToString());
                    }
                });
            }
            );
        }
    }
}
