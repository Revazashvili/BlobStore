using System.IO;

namespace API.Models.Responses
{
    /// <summary>
    /// Represents Blob response type.
    /// </summary>
    public class GetBlobResponse
    {
        /// <summary>
        /// Gets or sets blob content.
        /// </summary>
        public Stream Content { get; set; }
        /// <summary>
        /// Gets or sets blob content type.
        /// </summary>
        public string ContentType { get; set; }
    }
}