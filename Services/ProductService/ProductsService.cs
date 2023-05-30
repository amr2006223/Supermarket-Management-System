using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public IEnumerable<products> GetProducts()
        {
            return _productsRepository.GetProducts();
        }

        public async Task<bool> CreateProduct(products product)
    {
        return await _productsRepository.CreateProduct(product);
    }
    public async Task<products> GetProductById(Guid id)
    {
        return await _productsRepository.GetProductById(id);
    }
     public async Task<bool> UpdateProduct(products product)
    {
        return await _productsRepository.UpdateProduct(product);
    }

    public async Task<bool> DeleteProduct(Guid? id)
    {
        return await _productsRepository.DeleteProduct(id);
    }

    }
}
