using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Blob
{
    [Route(BlobRoutes.Get)]
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IResponse<IReadOnlyList<string>>>
    {
        private readonly IBlobService _blobService;

        public Get(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public override Task<ActionResult<IResponse<IReadOnlyList<string>>>> HandleAsync(CancellationToken cancellationToken = new())
        {
            return null;
        }
    }
}