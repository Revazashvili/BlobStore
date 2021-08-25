using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using API.Services.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [SwaggerOperation(Description = "Returns all existing container name from blob storage",
            Summary = "Return all container",
            OperationId = "Container.List",
            Tags = new[] {"Container"})]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved all container name from blob storage.",
            typeof(IAsyncEnumerable<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "No container were found in blob storage",
            typeof(IAsyncEnumerable<string>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IAsyncEnumerable<string>>> HandleAsync(
            CancellationToken cancellationToken = new()) => Ok(_container.GetAsync());
    }
}