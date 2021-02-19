using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PncUniform.Shopping.UniformInventory.Domain.Entities
{
    public class Uniforms
    {
        [Key]
        [Required]
        public int UniformId { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Stock { get; set; }

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
