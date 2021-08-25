using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Responses;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.GetAll)]
    public class GetAll : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<IAsyncEnumerable<GetBlobResponse>>
    {
        private readonly IBlobService _blobService;
        public GetAll(IBlobService blobService) => _blobService = blobService;
        
        [HttpGet]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<IAsyncEnumerable<GetBlobResponse>>> HandleAsync([FromQuery]string container,
            CancellationToken cancellationToken = new())
        {
            return Ok(_blobService.GetAllAsync(container, cancellationToken));
        }
    }
}