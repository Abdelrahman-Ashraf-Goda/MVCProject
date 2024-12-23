using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DAL.Interface;
using WebApplication.DAL.Models;

namespace WebApplication.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductModel> _productRepository;
        public ProductService(IRepository<ProductModel> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductModel>> GetAvailableProductsAsync(DateTime currentTime)
        {
            return (await _productRepository.GetAllAsync())
                .Where(p => currentTime >= p.StartDate && currentTime <= p.StartDate.AddDays(p.DurationInDay));
        }

        public async Task<ProductModel> GetProductByIdAsync(int id) => await _productRepository.GetByIdAsync(id);

        public async Task AddProductAsync(ProductModel product) => await _productRepository.AddAsync(product);

        public async Task UpdateProductAsync(ProductModel product, string updatedByUserId)
        {
            product.UserId = updatedByUserId;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id) => await _productRepository.DeleteAsync(id);


    }
}
