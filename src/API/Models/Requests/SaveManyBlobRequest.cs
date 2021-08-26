using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.Models.Requests
{
    public class SaveManyBlobRequest
    {
        public string Container { get; set; }
        public IEnumerable<IFormFile> Blobs { get; set; }
    }
}