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

        public void AddProductToBill(bill_items_details billItem)
        {
            _context.bill_items_details.Add(billItem);
        }

        public bills Createbill(bills bill)
        {
            try
            {
                _context.bill.Add(bill);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }
            return bill;
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
            catch (Exception e)
            {
                return false;
            }
        }

        public bills getBillById(Guid? id)
        {
            return _context.bill.FirstOrDefault(b => b.Id == id);
        }

        public bill_items_details getBillItem(Guid productId, Guid billId)
        {
            return _context.bill_items_details.FirstOrDefault(b => b.ProductId == productId && b.BillId == billId);
        }

        public IEnumerable<bill_items_details> getProductsInBill(Guid? id)
        {
            return _context.bill_items_details.Where(b => b.BillId == id).Include(b => b.Product).ToList();
        }

        public bills UpdateBill(Guid? id)
        {
            throw new NotImplementedException();
        }


        public bool RemoveProductFromBill(bill_items_details billItem)
        {
            try
            {
                _context.bill_items_details.Remove(billItem);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
