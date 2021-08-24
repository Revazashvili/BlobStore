using System;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Models.Responses;
using API.Routes;
using API.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints
{
    [Route(BlobRoutes.Save)]
    public class Save : BaseAsyncEndpoint
        .WithRequest<SaveBlobRequest>
        .WithResponse<Uri>
    {
        private readonly IBlobService _blobService;

        public Save(IBlobService blobService) => _blobService = blobService;

        [HttpPost]
        [SwaggerOperation(Description = "Returns all weather forecast",
            Summary = "Returns all weather forecast",
            OperationId = "Blob.Save",
            Tags = new []{ "Blob" })]
        [SwaggerResponse(StatusCodes.Status200OK,"All Weather Forecast Retrieved From Database.",typeof(GetBlobResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No Weather Forecast Were Found",typeof(GetBlobResponse))]
        public override async Task<ActionResult<Uri>> HandleAsync([FromForm]SaveBlobRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _blobService.SaveAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}