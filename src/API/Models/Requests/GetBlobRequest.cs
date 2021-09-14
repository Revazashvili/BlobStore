namespace API.Models.Requests
{
    /// <summary>
    /// Represents request to get blob.
    /// </summary>
    public class GetBlobRequest
    {
        /// <summary>
        /// Initializes a new instance of the GetBlobRequest class.
        /// </summary>
        public GetBlobRequest() { }
        
        /// <summary>
        /// Initializes a new instance of the GetBlobRequest class.
        /// </summary>
        /// <param name="blob">The blob name.</param>
        /// <param name="container">The container name.</param>
        public GetBlobRequest(string blob, string container) => (Blob, Container) = (blob, container);

        /// <summary>
        /// Gets or sets blob name.
        /// </summary>
        public string Blob { get; set; }
        
        /// <summary>
        /// Gets or sets container name.
        /// </summary>
        public string Container { get; set; }
    }
}