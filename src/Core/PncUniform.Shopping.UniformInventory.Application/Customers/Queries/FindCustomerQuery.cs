using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;
using FluentValidation;

namespace PncUniform.Shopping.UniformInventory.Application.Customers.Queries
{
    public class FindCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public string Name { get; set; }
    }

    public class FindCustomerQueryValidator : AbstractValidator<FindCustomerQuery>
    {
        public FindCustomerQueryValidator()
        {
            RuleFor(q => q.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        }
    }

    public class FindCustomerQueryHandler : IRequestHandler<FindCustomerQuery, IEnumerable<Customer>>
    {
        private readonly ILogger<FindCustomerQueryHandler> _logger;
        private readonly UniformManagementContext _dbContext;

        public FindCustomerQueryHandler(
            ILogger<FindCustomerQueryHandler> logger,
            UniformManagementContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public Task<IEnumerable<Customer>> Handle(FindCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finding customer with name {name}", request.Name);
            
            var customers = _dbContext.Customers.Where(customer => customer.Name.Contains(request.Name)).AsEnumerable();
            return Task.FromResult(customers);
        }
    }
}
