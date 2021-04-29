using System.ComponentModel.DataAnnotations;

namespace PncUniform.Shopping.UniformInventory.Application.Domain.Entities
{
    public class Uniform
    {
        [Key]
        [Required]
        public int UniformId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockLevel { get; set; }

        [Required]
        [StringLength(40)]
        public string Campus { get; set; }

        [Required]
        [StringLength(13)]
        public string Barcode { get; set; }

        [Required]
        [StringLength(13)]
        public string VendorBarcode { get; set; }
    }
}