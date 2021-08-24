using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.DTOs.Responses;
using Application.Common.Interfaces;
using Application.Common.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IResponse<GetBlobResponse>> GetAsync(string blob,string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = blobContainer.GetBlobClient(blob);
            var downloadedContent = await blobClient.DownloadAsync();
            var blobDto = new GetBlobResponse
            {
                Content = downloadedContent.Value.Content,
                ContentType = downloadedContent.Value.ContentType
            };
            return Response.Success(blobDto);
        }

        public async Task<IResponse<IReadOnlyList<GetBlobResponse>>> GetAllAsync(string container)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse<Uri>> SaveAsync(string container, IFormFile blob,string? blobName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = blobContainer.GetBlobClient(blob.FileName);
            var result = await blobClient.UploadAsync(blob.OpenReadStream());
            return Response.Success(new Uri(""));
        }

        public Task<IResponse<IReadOnlyList<Uri>>> SaveAsync(string container, IEnumerable<IFormFile> blobs)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string blob)
        {
            throw new NotImplementedException();
        }
    }
}