using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories;
using Supermarket_Managment_System.ViewModels;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public ProductsService(IProductsRepository productsRepository,ICategoriesRepository categoriesRepository)
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
        public IEnumerable<ProductsToBillVM> GetProductsWithCategories()
        {
            var products = _productsRepository.GetProducts();
            var categories = _categoriesRepository.GetCategories();

            List<ProductsToBillVM> productsToBillVM = new List<ProductsToBillVM>();

            foreach (var product in products)
            {
                var productCategory = _productsRepository.GetProductCategory(product.Id);

                if (productCategory != null)
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                        category = productCategory.Category
                    };
                    productsToBillVM.Add(productsToBill);
                }
                else
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                    };
                    productsToBillVM.Add(productsToBill);
                }
            }

            return productsToBillVM;
        }

    }
}
