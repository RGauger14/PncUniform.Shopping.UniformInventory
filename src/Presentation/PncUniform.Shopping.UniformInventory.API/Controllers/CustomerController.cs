using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PncUniform.Shopping.UniformInventory.Application.Customers.Commands;
using PncUniform.Shopping.UniformInventory.Application.Customers.Queries;

namespace PncUniform.Shopping.UniformInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerAsync()
        {
            await Task.CompletedTask.ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            await _mediator.Send(createCustomerCommand);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindCustomerQueryAsync([FromQuery] FindCustomerQuery findCustomerQuery)
        {
            var customers = await _mediator.Send(findCustomerQuery);
            return Ok(customers);
        }

    }
}