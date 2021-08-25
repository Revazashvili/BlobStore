using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Azure.Storage.Blobs;

namespace API.Services.Implementations
{
    /// <inheritdoc cref="IContainerService"/>
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        
        public async IAsyncEnumerable<string> GetAsync()
        {
            await foreach (var container in _blobServiceClient.GetBlobContainersAsync()) 
                yield return container.Name;
        }

        public async Task<bool> DeleteAsync(string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            return (await blobContainer.DeleteIfExistsAsync()).Value;
        }
    }
}