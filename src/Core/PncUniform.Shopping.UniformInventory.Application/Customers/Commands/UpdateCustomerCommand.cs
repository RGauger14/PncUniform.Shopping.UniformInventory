using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Linq;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;

namespace PncUniform.Shopping.UniformInventory.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public int CustomerId { get; set;  }

        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
    }

    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.MobileNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
        }
    }

    public class UpdateCustomerCommandHandler : AsyncRequestHandler<UpdateCustomerCommand>
    {
        private readonly UniformManagementContext _dbContext;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(
            ILogger<UpdateCustomerCommandHandler> logger,
            UniformManagementContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == request.CustomerId);
            
            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.MobileNumber = request.MobileNumber;
            


            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Updated Customer");
        }
    }
}