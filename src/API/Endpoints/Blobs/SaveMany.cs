using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Blobs;

[Route(BlobRoutes.SaveMany)]
public class SaveMany : BaseAsyncEndpoint
    .WithRequest<SaveManyBlobRequest>
    .WithResponse<IAsyncEnumerable<string>>
{
    private readonly IBlobService _blobService;
    public SaveMany(IBlobService blobService) => _blobService = blobService;
        
    /// <summary>
    /// Save blobs
    /// </summary>
    /// <remarks>
    /// Saves blobs into blob storage if doesn't exists blob with provided name already.
    /// </remarks>
    /// <param name="saveBlobRequests">The request to save blobs.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
    /// <response code="200">Urls of resources.</response>
    [HttpPost]
    [SwaggerOperation(Tags = new []{"Blob"})]
    public override async Task<ActionResult<IAsyncEnumerable<string>>> HandleAsync([FromForm]SaveManyBlobRequest saveBlobRequests, 
        CancellationToken cancellationToken = new()) =>
        Ok(_blobService.SaveAsync(saveBlobRequests, cancellationToken));
}