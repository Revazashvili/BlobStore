using Microsoft.AspNetCore.Http;

namespace API.Models.Requests
{
    public class SaveBlobRequest
    {
        public IFormFile Blob { get; set; }
        public string? BlobName { get; set; }
        public string Container { get; set; }
    }
}