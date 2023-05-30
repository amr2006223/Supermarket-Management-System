using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories.BillRepository
{
    public interface IBillRepository
    {
        public bills Createbill(bills bill);
        public bills getBillById(Guid? id);
        public IEnumerable<bill_items_details> getProductsInBill(bill_items_details billItem);
        public bool deleteBill(Guid? id);
        public bills UpdateBill(Guid? id);
        public bill_items_details getBillItem(Guid productId, Guid billId);
        public void AddProductToBill(Guid? productId);
        public bool RemoveProductFromBill(bill_items_details billItem);
        public void SaveChanges();
    }
}
