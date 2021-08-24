using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.DTOs.Responses;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IBlobService
    {
        Task<IResponse<GetBlobResponse>> GetAsync(string blob,string container);
        Task<IResponse<IReadOnlyList<GetBlobResponse>>> GetAllAsync(string container);
        Task<IResponse<Uri>> SaveAsync(string container, IFormFile blob,string? blobName);
        Task<IResponse<IReadOnlyList<Uri>>> SaveAsync(string container, IEnumerable<IFormFile> blobs);
        Task<bool> DeleteAsync(string blob);
    }
}