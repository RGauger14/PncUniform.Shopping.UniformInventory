using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PncUniform.Shopping.UniformInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniformController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UniformController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUniformAsync()
        {
            await Task.CompletedTask.ConfigureAwait(false);
            return Ok();
        }
    }
}
