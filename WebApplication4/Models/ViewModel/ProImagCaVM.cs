using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models.ViewModel
{
    public class ProImagCaVM
    {
        public int Id { get; set; }


        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(50)]
        public string? discription { get; set; }
        public int QuantityInStock { get; set; }

        public IEnumerable<Image> Image { get; set; }

        public IEnumerable<string> categorys { get; set; }
    }
}
