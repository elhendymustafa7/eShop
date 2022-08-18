namespace WebApplication4.Models.ViewModel
{
    public class ProductImageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string description { get; set; }
        public int QuantityInStock { get; set; }
        //  public int OrederQuantity { get; set; }
        public List<Category> Categorys { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
