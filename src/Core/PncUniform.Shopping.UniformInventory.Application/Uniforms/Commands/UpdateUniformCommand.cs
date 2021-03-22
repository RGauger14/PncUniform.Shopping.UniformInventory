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
    public class UpdateUniformCommand : IRequest
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

    public class UpdateUniformCommandValidator : AbstractValidator<CreateUniformCommand>
    {
        public UpdateUniformCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(u => u.Size).NotEmpty().MaximumLength(15).NotNull();
            RuleFor(u => u.Price).NotEmpty().NotNull();
            RuleFor(u => u.StockLevel).NotEmpty().NotNull();
            RuleFor(u => u.Campus).NotEmpty().NotNull();
            RuleFor(u => u.Barcode).MaximumLength(13).MinimumLength(13);
            RuleFor(u => u.VendorBarcode).MaximumLength(13).MinimumLength(13);
        }
    }
    public class UpdateUniformCommandHandler : AsyncRequestHandler<UpdateUniformCommand>
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

        protected override async Task Handle(UpdateUniformCommand request, CancellationToken cancellationToken)
        {
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

        }
    }
}
