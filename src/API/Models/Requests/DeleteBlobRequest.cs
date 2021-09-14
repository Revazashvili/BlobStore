namespace API.Models.Requests
{
    /// <summary>
    /// Represents request to Delete blob.
    /// </summary>
    public class DeleteBlobRequest
    {
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