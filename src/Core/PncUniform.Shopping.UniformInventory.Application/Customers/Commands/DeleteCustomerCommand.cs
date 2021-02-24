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

namespace PncUniform.Shopping.UniformInventory.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest 
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
    }

    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.MobileNumber).NotEmpty().MinimumLength(8).MaximumLength(15);

            RuleFor(c => c.Email).Must((_, email) => dbContext.Customers.Any(c => c.Email == email)).WithMessage("Cannot delete customer with email as it does not exist");
        }
    }

    public class DeleteCustomerCommandHandler : AsyncRequestHandler<DeleteCustomerCommand>
    {
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly UniformManagementContext _dbContext;

        public DeleteCustomerCommandHandler(
            ILogger<DeleteCustomerCommandHandler> logger,
            UniformManagementContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Removing Customer with name {name}", request.Name);

            var customerToDelete = _dbContext.Customers.Where(
            customer => customer.Name.Equals(request.Name)).AsEnumerable();
            _dbContext.Customers.Remove((Domain.Entities.Customer)customerToDelete);
            
            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Deleted customer");


        }
    }

}
