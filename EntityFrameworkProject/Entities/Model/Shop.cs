using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkProject.Entities.Model
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
