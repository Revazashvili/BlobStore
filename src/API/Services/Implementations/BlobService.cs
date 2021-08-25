using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using API.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace API.Services.Implementations
{
    /// <inheritdoc cref="IBlobService"/>
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<BlobService> _logger;

        public BlobService(BlobServiceClient blobServiceClient, ILogger<BlobService> logger) =>
            (_blobServiceClient, _logger) = (blobServiceClient, logger);

        public async IAsyncEnumerable<string> GetBlobsAsync(string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            await foreach (var blob in blobContainer.GetBlobsAsync())
                yield return blob.Name;
        }

        public async Task<GetBlobResponse> GetAsync(GetBlobRequest request)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            var blobClient = blobContainer.GetBlobClient(request.Blob);
            var downloadedContent = await blobClient.DownloadAsync();
            return new GetBlobResponse(downloadedContent?.Value?.Content, downloadedContent?.Value?.ContentType);
        }

        public async IAsyncEnumerable<GetBlobResponse> GetAllAsync(string container)
        {
            await foreach (var blob in GetBlobsAsync(container))
                yield return await GetAsync(new GetBlobRequest(blob, container));
        }

        public async Task<Uri?> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, null,cancellationToken);
            var blobName = string.IsNullOrEmpty(request.BlobName) ? $"{request.Blob.FileName}-{Guid.NewGuid():N}" : request.BlobName;
            var blobClient = blobContainer.GetBlobClient(blobName);
            if (await blobClient.ExistsAsync(cancellationToken))
            {
                _logger.LogInformation("Blob with name {blobName} already exists.", blobName);
                return null;
            }
            await blobClient.UploadAsync(request.Blob.OpenReadStream(), cancellationToken);
            return blobClient.Uri;
        }

        public async IAsyncEnumerable<Uri?> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            foreach (var saveBlobRequest in saveBlobRequests)
                yield return await SaveAsync(saveBlobRequest, cancellationToken);
        }

        public async Task<bool> DeleteAsync(DeleteBlobRequest request)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            return (await blobContainer.DeleteBlobIfExistsAsync(request.Blob)).Value;
        }
    }
}