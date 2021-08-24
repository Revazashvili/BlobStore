using System.IO;

namespace API.Models.Responses
{
    public class GetBlobResponse
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}