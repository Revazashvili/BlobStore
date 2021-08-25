using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
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
        [SwaggerOperation(Description = "Returns all weather forecast",
            Summary = "Returns all weather forecast",
            OperationId = "Blob.Get",
            Tags = new []{ "Blob" })]
        [SwaggerResponse(StatusCodes.Status200OK,"All Weather Forecast Retrieved From Database.",typeof(GetBlobResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No Weather Forecast Were Found",typeof(GetBlobResponse))]
        public override async Task<ActionResult<GetBlobResponse>> HandleAsync([FromQuery]GetBlobRequest request, CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.GetAsync(request));
        }
    }
}