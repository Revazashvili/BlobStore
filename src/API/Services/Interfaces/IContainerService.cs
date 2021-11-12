using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Services.Interfaces;

/// <summary>
/// Service for manipulating with containers.
/// </summary>
public interface IContainerService
{
    /// <summary>
    /// Returns all container name from blob storage.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns><see cref="IAsyncEnumerable{T}"/></returns>
    IAsyncEnumerable<string> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Deletes container from blob storage.
    /// </summary>
    /// <param name="container">The container name.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <returns>True if container is deleted, otherwise false.</returns>
    Task<bool> DeleteAsync(string container,CancellationToken cancellationToken);
}