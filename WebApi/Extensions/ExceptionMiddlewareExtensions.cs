using Entity.ErrorModel;
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
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contexeFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contexeFeature is not null)
                    {
                        logger.LogError($"Something went wrong: {contexeFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails() {
                        StatusCode=context.Response.StatusCode,
                        Message="Internal Server Error"
                        }.ToString());
                    }
                });
            }
            );
        }
    }
}
