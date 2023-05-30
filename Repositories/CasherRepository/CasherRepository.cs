using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories
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

            return _db.product_catoegories
                .Include(pc => pc.Category)
                .FirstOrDefault(pc => pc.ProductId == productId);

        }

        public Guid GetDefaultPaymentMethodId()
        {
            return _db.payment.FirstOrDefault().Id;
        }


        public void CreateBill(bills bill)
        {
            _db.bill.Add(bill);
            _db.SaveChanges();
        }

        public bill_items_details GetBillItem(Guid productId, Guid billId)
        {
            return _db.bill_items_details
                .FirstOrDefault(b => b.ProductId == productId && b.BillId == billId);
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


        public IEnumerable<bills> GetBillsList()
        {
            var bills = _db.bill.Include(b => b.User).Include(b => b.Payment).ToList();
            return bills;
        }

        public bills GetBillById(Guid id)
        {
            return _db.bill.FirstOrDefault(b => b.Id == id);
        }

        public void DeleteBill(bills bill)
        {
            _db.bill.Remove(bill);
            _db.SaveChanges();
        }
        public IEnumerable<bill_items_details> GetBillItems(Guid billId)
        {
            return _db.bill_items_details
                .Include(b => b.Product)
                .Where(b => b.BillId == billId)
                .ToList();
        }
        public bool UpdateQuantity(Guid billItemId, int quantity)
        {
            var billItem = _db.bill_items_details.FirstOrDefault(b => b.Id == billItemId);
            if (billItem == null)
            {
                return false;
            }

            // Get the original quantity of the bill item
            int originalQuantity = billItem.Quantity;

            // Update the quantity of the bill item
            billItem.Quantity = quantity;
            _db.SaveChanges();

            // Calculate the difference in quantity
            int quantityDifference = quantity - originalQuantity;

            // Update the total price of the bill
            var bill = _db.bill.FirstOrDefault(b => b.Id == billItem.BillId);
            if (bill == null)
            {
                return false;
            }

            // Check if the Product property is null for the bill item
            if (billItem.Product == null)
            {
                // Load the related Product entity
                _db.Entry(billItem).Reference(b => b.Product).Load();
            }

            // Check if the Product is now loaded or still null
            if (billItem.Product != null)
            {
                // Adjust the total price based on the quantity difference
                bill.TotalPrice += billItem.Product.Price * quantityDifference;
                _db.SaveChanges();
                return true;
            }

            // Handle the case where the Product is null
            // You may choose to display an error message or handle it as per your requirements
            return false;
        }

        public bool DeleteItem(Guid id)
        {
            var billItem = _db.bill_items_details.FirstOrDefault(b => b.Id == id);
            if (billItem == null)
            {
                return false;
            }

            _db.bill_items_details.Remove(billItem);
            _db.SaveChanges();
            return true;
        }

    }
}
