using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Containers
{
    [Route(ContainerRoutes.List)]
    public class List : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IAsyncEnumerable<string>>
    {
        private readonly IContainerService _container;

        public List(IContainerService container) => _container = container;

        /// <summary>
        /// Return all container name
        /// </summary>
        /// <remarks>
        /// Returns all container name from blob storage.
        /// </remarks>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Container"})]
        [Produces(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IAsyncEnumerable<string>>> HandleAsync(
            CancellationToken cancellationToken = new())
        {
            return Ok(_container.GetAsync());
        }

    }
}