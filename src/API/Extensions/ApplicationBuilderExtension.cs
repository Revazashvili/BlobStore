using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace API.Extensions
{
    /// <summary>
    /// Extension class for <see cref="IApplicationBuilder"/> interface.
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Adds a exception handler to the pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> interface.</param>
        public static void AddExceptionHandler<T>(this IApplicationBuilder app, ILogger<T> logger) 
            where T : class
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var result = JsonSerializer.Serialize(
                        $"{contextFeature.Error?.Message} {contextFeature.Error?.InnerException?.Message}");
                    logger.LogError("Error occured {error} {@result}", contextFeature.Error, result);
                });
            });
        }
    }
}