using System;
using System.Collections.Generic;
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
    [Route(BlobRoutes.SaveMany)]
    public class SaveMany : BaseAsyncEndpoint
        .WithRequest<IEnumerable<SaveBlobRequest>>
        .WithResponse<IAsyncEnumerable<Uri>>
    {
        private readonly IBlobService _blobService;
        public SaveMany(IBlobService blobService) => _blobService = blobService;
        
        [HttpPost]
        [SwaggerOperation(Tags = new []{"Blob"})]
        public override async Task<ActionResult<IAsyncEnumerable<Uri>>> HandleAsync([FromForm]IEnumerable<SaveBlobRequest> saveBlobRequests, 
            CancellationToken cancellationToken = new())
        {
            return Ok(_blobService.SaveAsync(saveBlobRequests, cancellationToken));
        }
    }
}