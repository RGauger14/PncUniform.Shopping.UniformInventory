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

namespace PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands
{
    public class CreateUniformCommand : IRequest
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
            RuleFor(u => u.Price).NotEmpty().NotNull();
            RuleFor(u => u.StockLevel).NotEmpty().NotNull();
            RuleFor(u => u.Campus).NotEmpty().NotNull();
            RuleFor(u => u.Barcode).MaximumLength(13).MinimumLength(13);
            RuleFor(u => u.VendorBarcode).MaximumLength(13).MinimumLength(13);

        }
    }
    public class CreateUniformComandHandler : AsyncRequestHandler<CreateUniformCommand>
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

        protected override async Task Handle(CreateUniformCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Creating Uniform");
            _dbContext.Uniforms.Add(new Domain.Entities.Uniform
            {
                Description = request.Description,
                Size = request.Size,
                Price = request.Price,
                StockLevel = request.StockLevel,
                Campus = request.Campus,
                Barcode = request.Barcode,
                VendorBarcode = request.VendorBarcode
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Created uniform");
        }
    }
}
