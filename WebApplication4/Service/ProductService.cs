using Microsoft.EntityFrameworkCore;
using System;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Models.ViewModel;

namespace WebApplication4.Service
{
        public class ProductService : IProductService
        {
            private readonly ApplicationDbContext _context;
            private readonly List<Category> categoryList = new List<Category>();
            private readonly List<Image> ImageList = new List<Image>();
            public ProductService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task AddSaveProduct(ProImagCaVM model, IFormFileCollection files)
            {
                var product = new Product
                {
                    Name = model.Name,
                    discription = model.discription,
                    QuantityInStock = model.QuantityInStock,
                    Price = model.Price,
                };
                product.Categorys = GetCategorysFromView(model, product);
                product.Images = GetImagesFromView(product, files);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            public async Task Edit(ProImagCaVM model, IFormFileCollection files, Product product)
            {


                product.Name = model.Name;
                product.Price = model.Price;
                product.QuantityInStock = model.QuantityInStock;
                product.discription = model.discription;
                product.Categorys = GetCategorysFromView(model, product);
                product.Images = GetImagesFromView(product, files);

                await _context.SaveChangesAsync();
            }

            public IEnumerable<Product> GetAllProducts() =>
                _context.Products
                .Include(i => i.Images)
                .Include(c => c.Categorys)
                .Where(p => p.QuantityInStock != 0)
                .ToList();

            public IEnumerable<UserIndexVM> GetAllProductsVM()
            {
                var products = GetAllProducts();
                var VM = new List<UserIndexVM>();
                foreach (var item in products)
                {
                    var x = new UserIndexVM
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Image = item.Images.FirstOrDefault().image
                    };
                    VM.Add(x);
                }
                return VM;
            }

            public ProImagCaVM GetAllPropretiyInProductbyId(int id)
            {
                var product = GetProductById(id);
                if (product == null) return null;
                var ViewModel = new ProImagCaVM
                {
                    Id = product.Id,
                    discription = product.discription,
                    Name = product.Name,
                    Price = product.Price,
                    QuantityInStock = product.QuantityInStock,
                    categorys = GetCatigoryDistinctList(),
                    Image = product.Images
                };
                return ViewModel;
            }

            public IEnumerable<UserIndexVM> GetAllUserIndexVM()
            {
                var products = GetAllProducts().Skip(0).Take(12);
                var VM = new List<UserIndexVM>();
                foreach (var item in products)
                {
                    var x = new UserIndexVM
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Image = item.Images.FirstOrDefault().image
                    };
                    VM.Add(x);
                }
                return VM;
            }

            public IEnumerable<string> GetCatigoryDistinctList()
                => new List<string> { "trousers", "Shirts", "Shoes", "Woman Bags", "Cameras", "Head Phone", "Sun Glasses", "Watches" }.OrderBy(x => x);

            public Product GetProductById(int id) =>
                _context.Products
                .Include(i => i.Images)
                .Include(c => c.Categorys)
                .FirstOrDefault(p => p.Id == id);




            private List<Category> GetCategorysFromView(ProImagCaVM model, Product product)
            {
                foreach (var cate in model.categorys)
                {
                    categoryList.Add(new Category { ProductId = product.Id, category = cate });
                }
                return categoryList;
            }
            private List<Image> GetImagesFromView(Product product, IFormFileCollection files)
            {
                foreach (var file in files)
                {
                    var Image = file;
                    using var dataStream = new MemoryStream();
                    Image.CopyToAsync(dataStream);
                    ImageList.Add(new Image { ProductId = product.Id, image = dataStream.ToArray() });
                }
                return ImageList;
            }

        }
    
}
