#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;

namespace API.Services
{
    public interface IBlobService
    {
        Task<GetBlobResponse> GetAsync(GetBlobRequest request);
        Task<IReadOnlyList<GetBlobResponse>> GetAllAsync(string container);
        Task<Uri?> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken);
        Task<IReadOnlyList<Uri>> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string container,string blob);
        Task<bool> DeleteAsync(string container);
    }
}