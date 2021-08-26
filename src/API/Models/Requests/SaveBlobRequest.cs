using Microsoft.AspNetCore.Http;

namespace API.Models.Requests
{
    /// <summary>
    /// Represents request to save blob.
    /// </summary>
    public class SaveBlobRequest
    {
        /// <summary>
        /// Initializes a new instance of the GetBlobRequest class.
        /// </summary>
        public SaveBlobRequest() { }

        /// <summary>
        /// Initializes a new instance of the GetBlobRequest class.
        /// </summary>
        /// <param name="blob"><inheritdoc cref="Blob"/></param>
        /// <param name="hasDefaultName"><inheritdoc cref="HasDefaultName"/></param>
        /// <param name="container"><inheritdoc cref="Container"/></param>
        public SaveBlobRequest(IFormFile blob, bool hasDefaultName, string container) =>
            (Blob, HasDefaultName, Container) = (blob, hasDefaultName, container);

        /// <summary>
        /// Gets or sets blob to save in blob storage.
        /// </summary>
        public IFormFile Blob { get; set; }
        /// <summary>
        /// Gets or sets flag indicating if blob will has default name or not.
        /// <remarks>
        /// Blob will has some unique name if this field is false,otherwise will take uploaded blob name.
        /// </remarks>
        /// </summary>
        public bool HasDefaultName { get; set; }
        /// <summary>
        /// Gets or sets container name.
        /// </summary>
        public string Container { get; set; }
    }
}