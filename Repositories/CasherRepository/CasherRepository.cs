using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
namespace Supermarket_Managment_System.Repositories
{
    public class CasherRepository : ICasherRepository
    {
        private readonly db_context _db;

        public CasherRepository(db_context db)
        {
            _db = db;
        }
        
        
        
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
