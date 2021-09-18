using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Azure.Storage.Blobs;
using Forbids;

namespace API.Services.Implementations
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IForbid _forbid;
        public ContainerService(BlobServiceClient blobServiceClient,IForbid forbid) =>
            (_blobServiceClient,_forbid) = (blobServiceClient,forbid);

        public async IAsyncEnumerable<string> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var container in _blobServiceClient.GetBlobContainersAsync(
                cancellationToken: cancellationToken))
                yield return container.Name;
        }

        public async Task<bool> DeleteAsync(string container,CancellationToken cancellationToken)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            _forbid.Null(blobContainer, new BlobContainerNotExistsException());
            return (await blobContainer.DeleteIfExistsAsync(cancellationToken:cancellationToken)!).Value;
        }
    }
}