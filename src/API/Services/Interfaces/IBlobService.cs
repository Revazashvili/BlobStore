using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;

namespace API.Services.Interfaces;

/// <summary>
/// Service for manipulating with blobs.
/// </summary>
public interface IBlobService
{
    /// <summary>
    /// Returns all blob name from container.
    /// </summary>
    /// <param name="container">The container name.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns><see cref="IAsyncEnumerable{T}"/></returns>
    IAsyncEnumerable<string> GetListAsync(string container,CancellationToken cancellationToken);

    /// <summary>
    /// Returns blob content and content type.
    /// </summary>
    /// <param name="request">The request object for retrieving blob.</param>
    /// <returns><see cref="string"/></returns>
    Task<string> GetAsync(GetBlobRequest request);
        
    /// <summary>
    /// Returns all blob from container.
    /// </summary>
    /// <param name="container">The container name.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns><see cref="IAsyncEnumerable{T}"/></returns>
    IAsyncEnumerable<string?> GetAllAsync(string container,CancellationToken cancellationToken);
        
    /// <summary>
    /// Saves blob into blob storage.
    /// </summary>
    /// <param name="request">Request object for saving blob.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>The uri of resource.</returns>
    Task<string> SaveAsync(SaveBlobRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Saves list of blob into blob storage.
    /// </summary>
    /// <param name="saveBlobRequests">Request object for saving blobs.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>The uris of resources.</returns>
    IAsyncEnumerable<string> SaveAsync(SaveManyBlobRequest saveBlobRequests,
        CancellationToken cancellationToken);
        
    /// <summary>
    /// Deletes blob from container.
    /// </summary>
    /// <param name="request">Request object to delete blob.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>True if successfully delete blob, otherwise false.</returns>
    Task<bool> DeleteAsync(DeleteBlobRequest request,CancellationToken cancellationToken);
        
    /// <summary>
    /// Downloads blob from blob storage
    /// </summary>
    /// <param name="downloadBlobRequest">Request object to download blob.</param>
    Task<DownloadBlobResponse> DownloadAsync(DownloadBlobRequest downloadBlobRequest);
}