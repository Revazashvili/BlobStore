namespace API.Models.Requests
{
    /// <summary>
    /// Get blob request object.
    /// </summary>
    public class GetBlobRequest
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