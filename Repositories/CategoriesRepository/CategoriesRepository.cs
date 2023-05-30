using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly db_context _db;

        public CategoriesRepository(db_context db)
        {
            _db = db;
        }

         public IEnumerable<categories> GetCategories()
    {
        return _db.category.ToList();
    }
    public bool AddCategory(categories category)
    {
        try
        {
            _db.category.Add(category);
            _db.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    }
}