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
    [Route(ContainerRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<bool>
    {
        private readonly IContainerService _container;

        public Delete(IContainerService container) => _container = container;
        
        [HttpDelete]
        [SwaggerOperation(Description = "Deletes container from blob storage",
            Summary = "Delete container",
            OperationId = "Container.Delete",
            Tags = new []{ "Container" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Successfully delete container from blob storage.",typeof(bool))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occured while deleting container from blob storage.",typeof(bool))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<bool>> HandleAsync([FromQuery]string container, CancellationToken cancellationToken = new())
        {
            var deleted = await _container.DeleteAsync(container);
            return deleted ? Ok(true) : BadRequest(false);
        }
    }
}