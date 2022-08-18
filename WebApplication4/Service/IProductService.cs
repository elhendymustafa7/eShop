using WebApplication4.Models;
using WebApplication4.Models.ViewModel;

namespace WebApplication4.Service
{
    public interface IProductService
    {
        IEnumerable<string> GetCatigoryDistinctList();
        Task AddSaveProduct(ProImagCaVM model, IFormFileCollection files);
        Product GetProductById(int id);
        ProImagCaVM GetAllPropretiyInProductbyId(int id);
        IEnumerable<UserIndexVM> GetAllUserIndexVM();
        IEnumerable<UserIndexVM> GetAllProductsVM();
        IEnumerable<Product> GetAllProducts();
        Task Edit(ProImagCaVM model, IFormFileCollection files, Product product);
    }
}
