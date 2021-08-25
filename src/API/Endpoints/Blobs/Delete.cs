using System.Threading;
using System.Threading.Tasks;
using API.Models.Requests;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Blobs
{
    [Route(BlobRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteBlobRequest>
        .WithResponse<bool>
    {
        private readonly IBlobService _blobService;
        public Delete(IBlobService blobService) => _blobService = blobService;

        [HttpDelete]
        public override async Task<ActionResult<bool>> HandleAsync([FromQuery]DeleteBlobRequest deleteBlobRequest,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.DeleteAsync(deleteBlobRequest, cancellationToken));
        }
    }
}