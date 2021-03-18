﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Queries;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUniformAsync([FromBody] CreateUniformCommand createUniformCommand)
        {
            await _mediator.Send(createUniformCommand);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindUnifromQueryAsync([FromQuery] FindUnifromQuery findUniformQuery)
        {
            var uniforms = await _mediator.Send(findUniformQuery);
            return Ok(uniforms);
        }
    }
}
