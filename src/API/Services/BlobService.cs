#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace API.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
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
                var resourceLink = new Uri($"https://{blobContainer.AccountName}.blob.core.windows.net/{request.Container}/{blobName}");
                return resourceLink;
            }

            return null;
        }

        public async Task<IReadOnlyList<Uri>> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string container,string blob)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> DeleteAsync(string container)
        {
            throw new NotImplementedException();
        }
    }
}