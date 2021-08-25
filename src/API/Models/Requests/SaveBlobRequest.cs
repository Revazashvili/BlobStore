using Microsoft.AspNetCore.Http;

namespace API.Models.Requests
{
    /// <summary>
    /// Represents request to save blob.
    /// </summary>
    public class SaveBlobRequest
    {
        /// <summary>
        /// Gets or sets blob to save in blob storage.
        /// </summary>
        public IFormFile Blob { get; set; }
        /// <summary>
        /// Gets or sets blob name.
        /// <remarks>
        /// Blob will have some unique name if this field is null or empty.
        /// </remarks>
        /// </summary>
        public string? BlobName { get; set; }
        /// <summary>
        /// Gets or sets container name.
        /// </summary>
        public string Container { get; set; }
    }
}