using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs;

[Route(BlobRoutes.GetAll)]
public class GetAll : BaseAsyncEndpoint
    .WithRequest<string>
    .WithResponse<IAsyncEnumerable<string?>>
{
    private readonly IBlobService _blobService;
    public GetAll(IBlobService blobService) => _blobService = blobService;
        
    /// <summary>
    /// Retrieve blobs
    /// </summary>
    /// <remarks>
    /// Retrieves blobs from blob storage if exists.
    /// </remarks>
    /// <param name="container">The container name.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <response code="200">Content and content type of blobs</response>
    [HttpGet]
    [SwaggerOperation(Tags = new []{"Blob"})]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public override async Task<ActionResult<IAsyncEnumerable<string?>>> HandleAsync([FromQuery]string container,
        CancellationToken cancellationToken = new()) =>
        Ok(_blobService.GetAllAsync(container, cancellationToken));
}