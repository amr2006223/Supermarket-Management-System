using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Services
{
    public class CasherServices
    {
        private readonly db_context _db;

        public CasherServices(db_context db)
        {
            _db=db;
        }

        public void createNewBillRecord()
        {
            bills bill = new bills();
            bill.Id = new Guid();
            bill.UserId = new Guid("902B72D0-42B2-4CD5-970A-08DB5CD4D842");
            bill.PaymentMethodId = new Guid("A7EE157E-AC7F-4F51-774A-08DB5CD6635E");
            bill.TotalPrice = 0;
            _db.bill.Add(bill);
            _db.SaveChanges();
        }
    }
}
