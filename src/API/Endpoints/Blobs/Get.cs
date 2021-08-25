using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.Get)]
    public class Get : BaseAsyncEndpoint
        .WithRequest<GetBlobRequest>
        .WithResponse<GetBlobResponse>
    {
        private readonly IBlobService _blobService;

        public Get(IBlobService blobService) => _blobService = blobService;

        /// <summary>
        /// Retrieve blob
        /// </summary>
        /// <remarks>
        /// Retrieves blob from blob storage if exists.
        /// </remarks>
        /// <param name="getBlobRequest">The request to retrieve blob.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <response code="200">Content and content type of blob</response>
        [HttpGet]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<GetBlobResponse>> HandleAsync([FromQuery]GetBlobRequest getBlobRequest,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.GetAsync(getBlobRequest));
        }
    }
}