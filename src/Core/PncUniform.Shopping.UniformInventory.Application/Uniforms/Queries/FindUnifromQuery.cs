using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Uniforms.Queries
{
    public class FindUnifromQuery : IRequest<IEnumerable<Uniform>>
    {
        public string Description { get; set; }
    }

    public class FindUniformQueryValidator : AbstractValidator<FindUnifromQuery>
    {
        public FindUniformQueryValidator()
        {
            RuleFor(q => q.Description).NotEmpty().MinimumLength(2).MaximumLength(100);
        }
    }

    public class FindUnifromQueryHandler : IRequestHandler<FindUnifromQuery, IEnumerable<Uniform>>
    {
        private readonly ILogger<FindUnifromQueryHandler> _logger;
        private readonly UniformManagementContext _dbContext;

        public FindUnifromQueryHandler(
            ILogger<FindUnifromQueryHandler> logger,
            UniformManagementContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Uniform>> Handle(FindUnifromQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finding unifrom with description {description}", request.Description);

            var uniforms = _dbContext.Uniforms.Where(uniforms => uniforms.Description.Contains(request.Description)).AsEnumerable();
            return Task.FromResult(uniforms);
        }
    }
}