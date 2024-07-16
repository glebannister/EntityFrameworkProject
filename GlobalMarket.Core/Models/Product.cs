using System.ComponentModel.DataAnnotations;

namespace GlobalMarket.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int ManufactureId { get; set; }
        public Manufacture Manufacture { get; set; }
    }
}
