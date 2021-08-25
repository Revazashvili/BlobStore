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

        [HttpGet]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<GetBlobResponse>> HandleAsync([FromQuery]GetBlobRequest getBlobRequest,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.GetAsync(getBlobRequest));
        }
    }
}