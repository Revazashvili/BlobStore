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
    [Route(ContainerRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<bool>
    {
        private readonly IContainerService _container;

        public Delete(IContainerService container) => _container = container;
        
        /// <summary>
        /// Delete container
        /// </summary>
        /// <remarks>
        /// Deletes container from blob storage if exists.
        /// </remarks>
        /// <param name="container">The container name.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <response code="200">True if successfully delete container, otherwise false.</response>
        [HttpDelete]
        [SwaggerOperation(Tags = new []{"Container"})]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<bool>> HandleAsync([FromQuery]string container,
            CancellationToken cancellationToken = new())
        {
            return Ok(await _container.DeleteAsync(container, cancellationToken));
        }
    }
}