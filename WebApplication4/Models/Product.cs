using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string discription { get; set; }
        public int QuantityInStock { get; set; }
        [Required]
        public int OrederQuantity { get; set; }
        [Required]
        public List<Category> Categorys { get; set; }
        [Required]
        public List<Image> Images { get; set; }
    }
}
