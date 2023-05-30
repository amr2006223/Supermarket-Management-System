using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories.CasherRepository
{
    public class CasherRepository : ICasherRepository
    {
        private readonly db_context _db;

        public CasherRepository(db_context db)
        {
            _db = db;
        }

        public IEnumerable<products> GetAllProducts()
        {
            return _db.product.ToList();
        }

        public IEnumerable<categories> GetAllCategories()
        {
            return _db.category.ToList();
        }

        public products_categories GetProductCategory(Guid productId)
        {
            return _db.product_catoegories.Include(pc => pc.Category).FirstOrDefault(pc => pc.ProductId == productId);
        }

        public Guid GetDefaultPaymentMethodId()
        {
            return _db.payment.FirstOrDefault().Id;
        }

        public bills GetBill(Guid billId)
        {
            return _db.bill.FirstOrDefault(b => b.Id == billId);
        }

        public products GetProduct(Guid productId)
        {
            return _db.product.Find(productId);
        }

        public products_offers GetProductOffer(Guid productId)
        {
            return _db.products_offers.FirstOrDefault(po => po.ProductId == productId);
        }

        public void AddProductToBill(bill_items_details billItem)
        {
            _db.bill_items_details.Add(billItem);
        }

        public void RemoveProductFromBill(bill_items_details billItem)
        {
            _db.bill_items_details.Remove(billItem);
        }

        public IEnumerable<bill_items_details> GetProductsInBill(Guid billId)
        {
            return _db.bill_items_details
                .Where(b => b.BillId == billId)
                .Include(b => b.Product)
                .ToList();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
