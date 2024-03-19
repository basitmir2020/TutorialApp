using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TutorialApp.Business.Common.Middleware.Exception;
using TutorialApp.Business.Common.ViewModel;

namespace TutorialApp.Business.Common.Middleware;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionMiddleware(this WebApplication app, ILogManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    await context.Response.WriteAsync(new ResponseViewModel
                    {
                        Success = false,
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error" + contextFeature.Error
                    }.ToString());
                }
            });
        });
    }
}