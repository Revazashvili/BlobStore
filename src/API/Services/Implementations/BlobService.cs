using System;
using System.Collections.Generic;
using System.IO;
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

        public async IAsyncEnumerable<string> GetListAsync(string container,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            await foreach (var blob in blobContainer.GetBlobsAsync(cancellationToken:cancellationToken))
                yield return blob.Name;
        }

        public async Task<string?> GetAsync(GetBlobRequest request)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            var blobClient = blobContainer.GetBlobClient(request.Blob);
            return blobClient.Uri.AbsoluteUri;
        }

        public async IAsyncEnumerable<string?> GetAllAsync(string container,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var blob in GetListAsync(container,cancellationToken))
                yield return await GetAsync(new GetBlobRequest(blob, container));
        }

        public async Task<string> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken)
        {
            var extension = Path.GetExtension(request.Blob.FileName);
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, null,cancellationToken);
            var blobName = request.HasDefaultName
                ? request.Blob.FileName
                : $"{Guid.NewGuid():N}{extension}";
            var blobClient = blobContainer.GetBlobClient(blobName);
            if (await blobClient.ExistsAsync(cancellationToken))
            {
                _logger.LogInformation("Blob with name {blobName} already exists.", blobName);
                return string.Empty;
            }
            await blobClient.UploadAsync(request.Blob.OpenReadStream(), cancellationToken);
            return blobClient.Uri.AbsoluteUri;
        }

        public async IAsyncEnumerable<string> SaveAsync(SaveManyBlobRequest saveBlobRequests,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            foreach (var blob in saveBlobRequests.Blobs)
                yield return await SaveAsync(new SaveBlobRequest(blob, true, saveBlobRequests.Container),
                    cancellationToken);
        }

        public async Task<bool> DeleteAsync(DeleteBlobRequest request,CancellationToken cancellationToken)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            return (await blobContainer.DeleteBlobIfExistsAsync(request.Blob,cancellationToken:cancellationToken)).Value;
        }

        public async Task<DownloadBlobResponse?> DownloadAsync(DownloadBlobRequest downloadBlobRequest)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(downloadBlobRequest.Container);
            var blobClient = blobContainer.GetBlobClient(downloadBlobRequest.Blob);
            if (await blobClient.ExistsAsync())
            {
                var downloadedContent = await blobClient.DownloadAsync();
                return new DownloadBlobResponse(blobClient.Name, downloadedContent.Value.Content,
                    downloadedContent.Value.ContentType);
            }

            return null;
        }
    }
}