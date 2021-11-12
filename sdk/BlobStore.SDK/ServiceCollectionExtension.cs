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
        var blobStoreOptions = BlobStoreOptions(options);

        services.AddHttpClient(nameof(BlobClient), 
            client => client.BaseAddress = new Uri(blobStoreOptions.Url));
        services.AddHttpClient(nameof(ContainerClient),
            client => client.BaseAddress = new Uri(blobStoreOptions.Url));
        
        services.Add(new ServiceDescriptor(typeof(IBlobClient), typeof(BlobClient), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(IContainerClient), typeof(ContainerClient), serviceLifetime));
    }

    /// <summary>
    /// Gets <see cref="BlobStoreOptions"/> from <see cref="Action{T}"/> and returns.
    /// </summary>
    /// <param name="options"><see cref="Action{T}"/></param>
    /// <returns><see cref="BlobStoreOptions"/></returns>
    private static BlobStoreOptions BlobStoreOptions(Action<BlobStoreOptions> options)
    {
        var blobStoreOptions = new BlobStoreOptions();
        options.Invoke(blobStoreOptions);
        return blobStoreOptions;
    }
}

public class BlobStoreOptions
{
    public string Url { get; set; }
}