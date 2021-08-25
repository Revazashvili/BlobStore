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
            var result = _blobServiceClient.GetBlobContainersAsync();
            await foreach (var item in result)
                yield return item.Name;
        }

        public async Task<bool> DeleteAsync(string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            return (await blobContainer.DeleteIfExistsAsync()).Value;
        }
    }
}