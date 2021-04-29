using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Uniforms.Queries
{
    public class FindAllUniformsQuery : IRequest<IEnumerable<Uniform>>
    {
    }

    public class FindAllUnifromsQueryHandler : IRequestHandler<FindAllUniformsQuery, IEnumerable<Uniform>>
    {
        private readonly ILogger<FindAllUnifromsQueryHandler> _logger;
        private readonly UniformManagementContext _dbContext;

        public FindAllUnifromsQueryHandler(
            ILogger<FindAllUnifromsQueryHandler> logger,
            UniformManagementContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Uniform>> Handle(FindAllUniformsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finding all uniforms");

            var allUniforms = _dbContext.Uniforms.AsEnumerable();
            return Task.FromResult(allUniforms);
        }
    }
}