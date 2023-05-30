using System.Collections.Generic;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories
{
    public interface ICategoriesRepository
    {
    IEnumerable<categories> GetCategories();
    bool AddCategory(categories category);

    }

}