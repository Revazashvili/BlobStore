using System.IO;

namespace API.Models.Responses
{
    /// <summary>
    /// Represents Blob response type.
    /// </summary>
    public class GetBlobResponse
    {
        /// <summary>
        /// Initializes a new instance of the GetBlobResponse class.
        /// </summary>
        /// <param name="content">The content of blob.</param>
        /// <param name="contentType">The content type of blob.</param>
        public GetBlobResponse(Stream? content, string? contentType) => (Content, ContentType) = (content, contentType);

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