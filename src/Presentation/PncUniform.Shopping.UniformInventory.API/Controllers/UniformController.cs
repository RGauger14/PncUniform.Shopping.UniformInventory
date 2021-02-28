using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUniformAsync([FromBody] CreateUniformCommand createUniformCommand)
        {
            await _mediator.Send(createUniformCommand);
            return Ok();
            
        }
        [HttpGet]
        public async Task<IActionResult> GetUniformAsync()
        {
            await Task.CompletedTask.ConfigureAwait(false);
            return Ok();
        }
    }
}
