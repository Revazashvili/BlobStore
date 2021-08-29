using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.List)]
    public class List : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<IAsyncEnumerable<string>>
    {
        private readonly IBlobService _blobService;
        public List(IBlobService blobService) => _blobService = blobService;
        
        /// <summary>
        /// All blob name
        /// </summary>
        /// <remarks>
        /// Retrieves all blob name from blob storage.
        /// </remarks>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <response code="200">List of blob names</response>
        [HttpGet]
        [SwaggerOperation(Tags = new []{"Blob"})]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IAsyncEnumerable<string>>> HandleAsync([FromQuery] string container,
            CancellationToken cancellationToken = new())
        {
            return Ok(_blobService.GetListAsync(container, cancellationToken));
        }
    }
}