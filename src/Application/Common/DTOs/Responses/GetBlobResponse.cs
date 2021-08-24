using System.IO;

namespace Application.Common.DTOs.Responses
{
    public class GetBlobResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}