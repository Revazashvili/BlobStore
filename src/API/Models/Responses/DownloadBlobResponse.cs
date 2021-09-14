using System.IO;

namespace API.Models.Responses
{
    /// <summary>
    /// Represents Download Blob type.
    /// </summary>
    public class DownloadBlobResponse
    {
        /// <summary>
        /// Initializes a new instance of the DownloadBlobResponse class.
        /// </summary>
        /// <param name="name">The blob name.</param>
        /// <param name="content">The blob content as <see cref="Stream"/>.</param>
        /// <param name="contentType">The blob content type.</param>
        public DownloadBlobResponse(string name, Stream content, string contentType) =>
            (Name, Content, ContentType) = (name, content, contentType);

        /// <summary>
        /// Gets or sets blob name. 
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets or sets blob uri.
        /// </summary>
        public Stream Content { get; }
        
        /// <summary>
        /// Gets or sets blob content type.
        /// </summary>
        public string ContentType { get; }
    }
}