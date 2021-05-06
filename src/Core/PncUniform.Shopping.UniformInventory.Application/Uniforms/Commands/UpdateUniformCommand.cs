using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands
{
    public class UpdateUniformCommand : IRequest<Uniform>
    {
        public int UniformId { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int StockLevel { get; set; }

        public string Campus { get; set; }

        public string Barcode { get; set; }

        public string VendorBarcode { get; set; }
    }

    public class UpdateUniformCommandValidator : AbstractValidator<UpdateUniformCommand>
    {
        public UpdateUniformCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(u => u.Size).NotEmpty().MaximumLength(15).NotNull();
            RuleFor(u => u.Price).GreaterThan(0);
            RuleFor(u => u.StockLevel).GreaterThanOrEqualTo(0);
            RuleFor(u => u.Campus).NotEmpty().NotNull();
            RuleFor(u => u.Barcode).MaximumLength(13).MinimumLength(13);
            RuleFor(u => u.VendorBarcode).MaximumLength(13).MinimumLength(13);
        }
    }

    public class UpdateUniformCommandHandler : IRequestHandler<UpdateUniformCommand, Uniform>
    {
        private readonly UniformManagementContext _dbContext;
        private readonly ILogger<UpdateUniformCommandHandler> _logger;

        public UpdateUniformCommandHandler(
        ILogger<UpdateUniformCommandHandler> logger,
        UniformManagementContext dbContext)

        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Uniform> Handle(UpdateUniformCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Updating Uniform");
            var uniform = _dbContext.Uniforms.FirstOrDefault(u => u.UniformId == request.UniformId);

            uniform.Description = request.Description;
            uniform.Size = request.Size;
            uniform.Price = request.Price;
            uniform.StockLevel = request.StockLevel;
            uniform.Campus = request.Campus;
            uniform.Barcode = request.Barcode;
            uniform.VendorBarcode = request.VendorBarcode;

            _dbContext.Uniforms.Update(uniform);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Updated Unifrom");

            return uniform;
        }
    }
}