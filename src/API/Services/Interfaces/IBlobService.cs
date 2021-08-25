#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Service for manipulating with blobs
    /// </summary>
    public interface IBlobService
    {
        /// <summary>
        /// Returns all blob name from container
        /// </summary>
        /// <param name="container">The container name</param>
        Task<IReadOnlyList<string>> GetBlobsAsync(string container);
        /// <summary>
        /// Returns blob content and content type
        /// </summary>
        /// <param name="request">The request object for retrieving blob</param>
        /// <returns><see cref="GetBlobResponse"/></returns>
        Task<GetBlobResponse> GetAsync(GetBlobRequest request);
        /// <summary>
        /// Returns all blob from container
        /// </summary>
        /// <param name="container">The container name</param>
        /// <returns><see cref="IReadOnlyList{T}"/></returns>
        Task<IReadOnlyList<GetBlobResponse>> GetAllAsync(string container);
        
        Task<Uri?> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken);
        Task<IReadOnlyList<Uri>> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(DeleteBlobRequest request);
    }
}