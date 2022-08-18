namespace WebApplication4.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public string category { get; set; }
    }
}
