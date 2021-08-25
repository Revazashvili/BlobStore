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
    [Route(BlobRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteBlobRequest>
        .WithResponse<bool>
    {
        private readonly IBlobService _blobService;
        public Delete(IBlobService blobService) => _blobService = blobService;

        /// <summary>
        /// Delete blob
        /// </summary>
        /// <remarks>
        /// Deletes blob from blob storage if exists.
        /// </remarks>
        /// <param name="deleteBlobRequest">The request to delete blob.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <response code="200">True if successfully delete blob, otherwise false.</response>
        [HttpDelete]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<bool>> HandleAsync([FromQuery]DeleteBlobRequest deleteBlobRequest,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.DeleteAsync(deleteBlobRequest, cancellationToken));
        }
    }
}