using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Service for manipulating with blobs.
    /// </summary>
    public interface IBlobService
    {
        /// <summary>
        /// Returns all blob name from container.
        /// </summary>
        /// <param name="container">The container name.</param>
        IAsyncEnumerable<string> GetBlobsAsync(string container);
        /// <summary>
        /// Returns blob content and content type.
        /// </summary>
        /// <param name="request">The request object for retrieving blob.</param>
        /// <returns><see cref="GetBlobResponse"/></returns>
        Task<GetBlobResponse> GetAsync(GetBlobRequest request);
        /// <summary>
        /// Returns all blob from container.
        /// </summary>
        /// <param name="container">The container name.</param>
        /// <returns><see cref="IReadOnlyList{T}"/></returns>
        IAsyncEnumerable<GetBlobResponse> GetAllAsync(string container);
        /// <summary>
        /// Saves blob into blob storage.
        /// </summary>
        /// <param name="request">Request object for saving blob.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <returns><see cref="Uri"/> of resource.</returns>
        Task<Uri?> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Saves list of blob into blob storage.
        /// </summary>
        /// <param name="saveBlobRequests">Request object for saving blobs.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <returns><see cref="Uri"/> of resources.</returns>
        IAsyncEnumerable<Uri?> SaveAsync(IEnumerable<SaveBlobRequest> saveBlobRequests,
            CancellationToken cancellationToken);
        /// <summary>
        /// Deletes blob from container.
        /// </summary>
        /// <param name="request">Request object for deleting blob.</param>
        /// <returns>True if successfully delete blob, otherwise false.</returns>
        Task<bool> DeleteAsync(DeleteBlobRequest request);
    }
}