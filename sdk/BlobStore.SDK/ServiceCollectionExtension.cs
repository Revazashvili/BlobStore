using System;
using Microsoft.Extensions.DependencyInjection;

namespace BlobStore.SDK;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/> interface.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Injects blob store sdk services into dependency injection container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> interface.</param>
    /// <param name="options"><see cref="BlobStoreOptions"/> object.</param>
    /// <param name="serviceLifetime">Service Lifetime</param>
    public static void AddBlobClient(this IServiceCollection services,Action<BlobStoreOptions> options,ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        var blobStoreOptions = new BlobStoreOptions();
        options.Invoke(blobStoreOptions);
            
        services.AddHttpClient(nameof(BlobClient), 
            client => client.BaseAddress = new Uri(blobStoreOptions.Url));
        services.AddHttpClient(nameof(ContainerClient),
            client => client.BaseAddress = new Uri(blobStoreOptions.Url));
            
        var blobClientServiceDescriptor =
            new ServiceDescriptor(typeof(IBlobClient), typeof(BlobClient), serviceLifetime);
        var containerClientServiceDescriptor =
            new ServiceDescriptor(typeof(IContainerClient), typeof(ContainerClient), serviceLifetime);
            
        services.Add(blobClientServiceDescriptor);
        services.Add(containerClientServiceDescriptor);
    }
}

public class BlobStoreOptions
{
    public string Url { get; set; }
}