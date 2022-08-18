using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4.Models.ViewModel;
using WebApplication4.Models;
using WebApplication4.Service;
using WebApplication4.Data;
using NToastNotify;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        #region readonly
        private readonly ApplicationDbContext _context;
        private readonly IProductService _service;
        private readonly IToastNotification _toastNotification;
        #endregion
        #region Ctor
        public ProductsController(ApplicationDbContext context, IToastNotification toastNotification, IProductService service)
        {
            _context = context;
            _toastNotification = toastNotification;
            _service = service;
        }
        #endregion
        #region Index
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Index(int Pg = 1)
        {
            const int pageSize = 5;

            if (Pg < 1) Pg = 1;

            int recsCount = _context.Products.Count();

            var pager = new Pager(recsCount, Pg, pageSize);

            int recSkip = (Pg - 1) * pageSize;

            var data = await _context.Products
                        .Include(i => i.Images)
                        .Include(c => c.Categorys)
                        .Skip(recSkip)
                        .Take(pager.PageSize)
                        .ToListAsync();

            this.ViewBag.Pager = pager;

            return View(data);
        }
        #endregion
        #region Create Get
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ViewModel = new ProImagCaVM
            {
                categorys = _service.GetCatigoryDistinctList(), // GetCatigoryDistinctList
            };
            return View(ViewModel);
        }
        #endregion
        #region Create Post
        [HttpPost]
        public async Task<IActionResult> Create(ProImagCaVM model)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Form.Files;

                await _service.AddSaveProduct(model, files);

                _toastNotification.AddSuccessToastMessage("Movie Created successfully");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion
        #region Edit Get
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            return _service.GetAllPropretiyInProductbyId(id) == null ? NotFound() : View(_service.GetAllPropretiyInProductbyId(id));
        }
        #endregion
        #region Edit Post
        [HttpPost]
        public async Task<IActionResult> Edit(ProImagCaVM model)
        {


            if (!ModelState.IsValid)
            {
                model.categorys = _service.GetCatigoryDistinctList();
                return View(model);
            }
            var product = _service.GetProductById(model.Id);
            if (product == null)
                return NotFound();

            var files = Request.Form.Files;

            await _service.Edit(model, files, product);

            _toastNotification.AddSuccessToastMessage("Product updated successfully");
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return BadRequest();

            var product = _service.GetProductById(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        [AllowAnonymous]
        public async Task<IActionResult> UserIndex()
        {
            var product = _service.GetAllUserIndexVM();
            return View(product);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Products(int Pg = 1)
        {
            const int pageSize = 6;

            if (Pg < 1) Pg = 1;

            int recsCount = _service.GetAllProductsVM().Count();

            var pager = new Pager(recsCount, Pg, pageSize);

            int recSkip = (Pg - 1) * pageSize;

            var data = _service
                        .GetAllProductsVM()
                        .Skip(recSkip)
                        .Take(pager.PageSize)
                        .ToList();

            this.ViewBag.Pager = pager;

            return View(data);
            //    var product = _service.GetAllProductsVM();
            //return View(product);
        }


        [AllowAnonymous]
        public async Task<IActionResult> About_Us()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Contact_Us()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> single_product(int id)
        {
            var product = _service.GetProductById(id);
            ViewBag.Products = _service.GetAllUserIndexVM();
            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> single_product(int id, int Quantity)
        {
            var product = _service.GetProductById(id);
            if (product.OrederQuantity > product.QuantityInStock)
                return View(_service.GetProductById(product.Id));
            product.QuantityInStock -= Quantity;
            await _context.SaveChangesAsync();
            ViewBag.Products = _service.GetAllUserIndexVM();

            _toastNotification.AddSuccessToastMessage("Oredering Product  successfully");
            return View(_service.GetProductById(product.Id));
        }

        #region Praivate Method


        #endregion
    }
}
