using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication.BLL.Service;
using WebApplication.DAL.Data;
using WebApplication.DAL.Models;

namespace WebApplication.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ApplicationDBContext _dbContext;
        private new List<string> _allowedEx = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        public ProductController(IProductService productService, ApplicationDBContext dbContext)
        {
            _productService = productService;
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProduct()
        {
            var currentdate = DateTime.Now;
            var allProduct = await _productService.GetAvailableProductsAsync(currentdate);
            return View(allProduct);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductModel model, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (model.ImagePath == null)
                    return BadRequest("Poster is required!");

                //if (model.ImagePath.Length > _maxAllowedPosterSize)
                //    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await files.CopyToAsync(dataStream);

                var newProduct = new ProductModel
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    CreatedTime = DateTime.Now,
                    UserId = model.UserId,
                    DurationInDay = model.DurationInDay,
                    ImagePath = dataStream.ToArray(),
                };

                _productService.AddProductAsync(newProduct);
                _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);

        }



        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductModel model)
        {
            var getProduct = await _dbContext.ProductModels.FindAsync(model.Id);

            if (getProduct == null) return BadRequest(new { title = $"there is product with this {model.Id}" });

                var updateProduct = new ProductModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    DurationInDay= model.DurationInDay,
                    UpdatedTime = DateTime.Now,
                    UserId = model.UserId,
                };

            if(updateProduct != null)
            {


                _productService.UpdateProductAsync(updateProduct);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(updateProduct);

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }



    }
}
