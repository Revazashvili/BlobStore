using System.IO;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.Download)]
    public class Download : BaseAsyncEndpoint
        .WithRequest<DownloadBlobRequest>
        .WithoutResponse
    {
        private readonly IBlobService _blobService;
        public Download(IBlobService blobService) => _blobService = blobService;
        
        /// <summary>
        /// Download blob
        /// </summary>
        /// <remarks>
        /// Downloads blob from blob storage if exists.
        /// </remarks>
        /// <param name="downloadBlobRequest">The request to download blob.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        [HttpGet]
        [SwaggerOperation(Tags = new []{"Blob"})]
        [ProducesResponseType(typeof(File),200)]
        public override async Task<ActionResult> HandleAsync([FromQuery]DownloadBlobRequest downloadBlobRequest, 
            CancellationToken cancellationToken = new())
        {
            var downloadBlobResponse = await _blobService.DownloadAsync(downloadBlobRequest);
            return File(downloadBlobResponse.Content, downloadBlobResponse.ContentType, downloadBlobResponse.Name);
        }
    }
}