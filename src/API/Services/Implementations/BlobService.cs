using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using API.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace API.Services.Implementations
{
    /// <inheritdoc cref="IBlobService"/>
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        
        public Task<IReadOnlyList<string>> GetBlobsAsync(string container)
        {
            throw new NotImplementedException();
        }

        public async Task<GetBlobResponse> GetAsync(GetBlobRequest request)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            var blobClient = blobContainer.GetBlobClient(request.Blob);
            var downloadedContent = await blobClient.DownloadAsync();
            var blobDto = new GetBlobResponse
            {
                Content = downloadedContent.Value.Content,
                ContentType = downloadedContent.Value.ContentType
            };
            return blobDto;
        }

        public async Task<IReadOnlyList<GetBlobResponse>> GetAllAsync(string container)
        {
            throw new NotImplementedException();
        }

        public async Task<Uri?> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, null,cancellationToken);
            var blobName = string.IsNullOrEmpty(request.BlobName) ? $"{request.Blob.FileName}-{Guid.NewGuid():N}" : request.BlobName;
            var blobClient = blobContainer.GetBlobClient(blobName);
            if (!await blobClient.ExistsAsync(cancellationToken))
            {
                await blobClient.UploadAsync(request.Blob.OpenReadStream(), cancellationToken);
                return blobClient.Uri;
            }
            return null;
        }

        public async Task<IReadOnlyList<Uri>> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests, CancellationToken cancellationToken)
        {
            List<Uri> uris = new();
            foreach (var saveBlobRequest in saveBlobRequests)
            {
                var uri = await SaveAsync(saveBlobRequest, cancellationToken);
                if (uri is not null) uris.Add(uri);
            }
            return uris.AsReadOnly();
        }

        public async Task<bool> DeleteAsync(DeleteBlobRequest request)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(request.Container);
            if (!await blobContainer.ExistsAsync()) return false;
            return (await blobContainer.DeleteBlobIfExistsAsync(request.Blob)).Value;
        }
    }
}