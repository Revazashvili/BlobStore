using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Service for manipulating with containers.
    /// </summary>
    public interface IContainerService
    {
        /// <summary>
        /// Returns all container name from blob storage.
        /// </summary>
        /// <returns><see cref="IAsyncEnumerable{T}"/></returns>
        IAsyncEnumerable<string> GetAsync();
        
        /// <summary>
        /// Deletes container from blob storage.
        /// </summary>
        /// <param name="container">The container name.</param>
        /// <returns>True if container is deleted, otherwise false.</returns>
        Task<bool> DeleteAsync(string container);
    }
}