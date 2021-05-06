using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PncUniform.Shopping.UniformInventory.Application.Db;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands
{
    public class CreateUniformCommand : IRequest<Uniform>
    {
        public string Description { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int StockLevel { get; set; }

        public string Campus { get; set; }

        public string Barcode { get; set; }

        public string VendorBarcode { get; set; }
    }

    public class CreateUnifromCommandValidator : AbstractValidator<CreateUniformCommand>
    {
        public CreateUnifromCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(u => u.Size).NotEmpty().MaximumLength(15).NotNull();
            RuleFor(u => u.Price).GreaterThan(0);
            RuleFor(u => u.StockLevel).GreaterThanOrEqualTo(0);
            RuleFor(u => u.Campus).NotEmpty().NotNull();
            RuleFor(u => u.Barcode).MaximumLength(13).MinimumLength(13);
            RuleFor(u => u.VendorBarcode).MaximumLength(13).MinimumLength(13);
        }
    }

    public class CreateUniformComandHandler : IRequestHandler<CreateUniformCommand, Uniform>
    {
        private readonly UniformManagementContext _dbContext;
        private readonly ILogger<CreateUniformComandHandler> _logger;

        public CreateUniformComandHandler(
        ILogger<CreateUniformComandHandler> logger,
        UniformManagementContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Uniform> Handle(CreateUniformCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Creating Uniform");
            var uniform = new Uniform
            {
                Description = request.Description,
                Size = request.Size,
                Price = request.Price,
                StockLevel = request.StockLevel,
                Campus = request.Campus,
                Barcode = request.Barcode,
                VendorBarcode = request.VendorBarcode
            };

            _dbContext.Uniforms.Add(uniform);
            await _dbContext.SaveChangesAsync();

            _logger.LogDebug("Created uniform");

            return uniform;
        }
    }
}