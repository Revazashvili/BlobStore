using Application.Common.Interfaces;
using Azure.Storage.Blobs;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Extension class for <see cref="IServiceCollection"/> interface
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Injects infrastructure dependencies into dependency injection container
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface</param>
        /// <param name="configuration"><see cref="IConfiguration"/> interface</param>
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));
            services.AddScoped<IApplicationDbContext>();
            services.AddScoped<IBlobService, BlobService>();
        }
    }
}