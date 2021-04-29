using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PncUniform.Shopping.UniformInventory.Application.Domain.Entities
{
    public class Order
    {
        [Key]
        [Required]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(150)]
        public string StudentName { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(100)]
        public string DeliveryOption { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; }

        [Required]
        public bool Payed { get; set; }
    }
}