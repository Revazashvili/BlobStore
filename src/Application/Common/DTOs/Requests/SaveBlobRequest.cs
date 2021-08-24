using Microsoft.AspNetCore.Http;

namespace Application.Common.DTOs.Requests
{
    public class SaveBlobRequest
    {
        public IFormFile Blob { get; set; }
        public string? BlobName { get; set; }
        public string Container { get; set; }
    }
}