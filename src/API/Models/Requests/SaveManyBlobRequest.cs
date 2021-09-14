using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Models.Requests
{
    /// <summary>
    /// Represents request to save many blob.
    /// </summary>
    public class SaveManyBlobRequest
    {
        /// <summary>
        /// Gets or sets container name.
        /// </summary>
        public string Container { get; set; }
        
        /// <summary>
        /// Gets or sets list of blobs.
        /// </summary>
        public IEnumerable<IFormFile> Blobs { get; set; }
    }
}