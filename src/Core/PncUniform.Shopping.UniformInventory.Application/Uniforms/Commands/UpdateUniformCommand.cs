using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
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

    public class UpdateUnifromCommandValidator : AbstractValidator<CreateUniformCommand>
    {
        public UpdateUnifromCommandValidator(UniformManagementContext dbContext)
        {
            RuleFor(u => u.Size).NotEmpty().MaximumLength(15).NotNull();
            RuleFor(u => u.Price).NotEmpty().NotNull();
            RuleFor(u => u.StockLevel).NotEmpty().NotNull();
            RuleFor(u => u.Campus).NotEmpty().NotNull();
            RuleFor(u => u.Barcode).MaximumLength(13).MinimumLength(13);
            RuleFor(u => u.VendorBarcode).MaximumLength(13).MinimumLength(13);
        }
    }
    
}
