using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services
{
    public interface ICategoriesService
    {
         IEnumerable<categories> GetCategories();
         bool AddCategory(categories category);
         IEnumerable<ProductsToBillVM> GetProductsWithCategories();
    }
}