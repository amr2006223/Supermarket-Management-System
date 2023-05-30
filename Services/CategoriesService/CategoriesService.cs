using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories;

using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProductsRepository _productRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public IEnumerable<categories> GetCategories()
    {
        return _categoriesRepository.GetCategories();
    }
        public bool AddCategory(categories category)
    {
        return _categoriesRepository.AddCategory(category);
    }
        public IEnumerable<ProductsToBillVM> GetProductsWithCategories()
        {
            var products = _productRepository.GetProducts();
            var categories = _categoriesRepository.GetCategories();

            List<ProductsToBillVM> productsToBillVM = new List<ProductsToBillVM>();

            foreach (var product in products)
            {
                var productCategory = _productRepository.GetProductCategory(product.Id);

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