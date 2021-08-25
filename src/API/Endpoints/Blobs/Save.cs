using System;
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
    [Route(BlobRoutes.Save)]
    public class Save : BaseAsyncEndpoint
        .WithRequest<SaveBlobRequest>
        .WithResponse<Uri>
    {
        private readonly IBlobService _blobService;
        public Save(IBlobService blobService) => _blobService = blobService;

        [HttpPost]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<Uri>> HandleAsync([FromForm]SaveBlobRequest saveBlobRequest,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _blobService.SaveAsync(saveBlobRequest, cancellationToken));
        }
    }
}