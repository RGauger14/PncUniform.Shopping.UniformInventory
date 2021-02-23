using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;

namespace PncUniform.Shopping.UniformInventory.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }
    }


    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.MobileNumber).NotEmpty().MinimumLength(8).MaximumLength(15);

            RuleFor(c => c.Email).Must((_, email) => !dbContext.Customers.Any(c => c.Email == email)).WithMessage("Cannot create customer with email as it already exists");
        }
    }

    public class CreateCustomerCommandHandler : AsyncRequestHandler<CreateCustomerCommand>
    {
        private readonly UniformManagementContext _dbContext;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(
            ILogger<CreateCustomerCommandHandler> logger,
            UniformManagementContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Creating customer");
            _dbContext.Customers.Add(new Domain.Entities.Customer
            {
                Name = request.Name,
                Email = request.Email,
                MobileNumber = request.MobileNumber
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Created customer");
        }
    }
}
