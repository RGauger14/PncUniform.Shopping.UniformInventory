using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Customers.Queries
{
    public class FindAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
        
    }

    public class FindAllCustomersQueryHandler : IRequestHandler<FindAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly ILogger<FindAllCustomersQueryHandler> _logger;
        private readonly UniformManagementContext _dbContext;

        public FindAllCustomersQueryHandler(
            ILogger<FindAllCustomersQueryHandler> logger,
            UniformManagementContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Customer>> Handle(FindAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finding all customers");

            var allCustomers = _dbContext.Customers.AsEnumerable();
            return Task.FromResult(allCustomers);
        }
    }
}
