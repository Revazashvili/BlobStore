using API.Services.Implementations;
using API.Services.Interfaces;
using Azure.Storage.Blobs;
using Forbids;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    /// <summary>
    /// Extension class for <see cref="IServiceCollection"/> interface.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds cross-origin resource sharing services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface.</param>
        /// <param name="configuration"><see cref="IConfiguration"/> interface.</param>
        /// <param name="policyName">Policy Name</param>
        /// <returns>A Reference to the same <see cref="IServiceCollection"/> instance.</returns>
        public static void ConfigureCors(this IServiceCollection services,IConfiguration configuration,string policyName)
        {
            var cors = configuration.GetSection("Cors").Get<string[]>();
            services.AddCors(options => options.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(cors)
                    .AllowCredentials();
            }));
        }

        /// <summary>
        /// Add Api needed dependencies into DI Container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface.</param>
        /// <param name="configuration"><see cref="IConfiguration"/> interface.</param>
        /// <returns>A Reference to the same <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddForbids();
            services.AddScoped(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IContainerService, ContainerService>();

            return services;
        }
    }
}