using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.List)]
    public class List : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<IAsyncEnumerable<string>>
    {
        private readonly IBlobService _blobService;
        public List(IBlobService blobService) => _blobService = blobService;
        
        [HttpGet]
        public override async Task<ActionResult<IAsyncEnumerable<string>>> HandleAsync([FromQuery] string container,
            CancellationToken cancellationToken = new())
        {
            return Ok(_blobService.GetListAsync(container, cancellationToken));
        }
    }
}