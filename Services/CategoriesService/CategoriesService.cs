using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

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

    }
}