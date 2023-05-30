using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Repositories.BillRepository
{
    public class BillRepository : IBillRepository
    {
        private readonly db_context _context;
        public BillRepository(db_context context)
        {
            _context = context;
        }
        public bills Createbill(bills bill)
        {
            try
            {
                _context.bill.Add(bill);
                _context.SaveChanges();
            }
            catch (SqlException e)
            {
                return null;
            }
            return bill;
        }
        public bills getBillById(Guid? id)
        {
            return _context.bill.FirstOrDefault(b => b.Id == id);
        }
        public IEnumerable<bill_items_details> getProductsInBill(Guid? id)
        {
            return _context.bill_items_details.Where(b => b.BillId == id).Include(b => b.Product).ToList();
        }
        public bool deleteBill(Guid? id)
        {
            bills bill = getBillById(id);
            if (bill == null) return false;
            try
            {
                _context.bill.Remove(bill);

                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }
        public bills UpdateBill(Guid? id)
        {
            throw new NotImplementedException();
        }
        public bill_items_details getBillItem(Guid productId, Guid billId)
        {
            return _context.bill_items_details.FirstOrDefault(b => b.ProductId == productId && b.BillId == billId);
        }
        public void AddProductToBill(bill_items_details billItem)
        {
            _context.bill_items_details.Add(billItem);
        }
        public bool RemoveProductFromBill(bill_items_details billItem)
        {
            try
            {
                _context.bill_items_details.Remove(billItem);

                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }
        public bool DeleteItem(Guid id)
        {
            var billItem = _context.bill_items_details.FirstOrDefault(b => b.Id == id);
            if (billItem == null)
            {
                return false;
            }

            _context.bill_items_details.Remove(billItem);
            _context.SaveChanges();
            return true;
        }
        public IEnumerable<bills> GetBillsList()
        {
            var bills = _context.bill.Include(b => b.User).Include(b => b.Payment).ToList();
            return bills;
        }
        public bool UpdateQuantity(Guid billItemId, int quantity)
        {
            var billItem = _context.bill_items_details.FirstOrDefault(b => b.Id == billItemId);
            if (billItem == null)
            {
                return false;
            }

            // Get the original quantity of the bill item
            int originalQuantity = billItem.Quantity;

            // Update the quantity of the bill item
            billItem.Quantity = quantity;
            _context.SaveChanges();

            // Calculate the difference in quantity
            int quantityDifference = quantity - originalQuantity;

            // Update the total price of the bill
            var bill = _context.bill.FirstOrDefault(b => b.Id == billItem.BillId);
            if (bill == null)
            {
                return false;
            }

            // Check if the Product property is null for the bill item
            if (billItem.Product == null)
            {
                // Load the related Product entity
                _context.Entry(billItem).Reference(b => b.Product).Load();
            }

            // Check if the Product is now loaded or still null
            if (billItem.Product != null)
            {
                // Adjust the total price based on the quantity difference
                bill.TotalPrice += billItem.Product.Price * quantityDifference;
                _context.SaveChanges();
                return true;
            }

            // Handle the case where the Product is null
            // You may choose to display an error message or handle it as per your requirements
            return false;
        }
        public IEnumerable<bill_items_details> GetBillItems(Guid billId)
        {
            return _context.bill_items_details
                .Include(b => b.Product)
                .Where(b => b.BillId == billId)
                .ToList();
        }
        public Guid GetDefaultPaymentMethodId()
        {
            return _context.payment.FirstOrDefault().Id;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
