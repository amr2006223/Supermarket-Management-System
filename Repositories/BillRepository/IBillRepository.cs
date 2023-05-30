using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories.BillRepository
{
    public interface IBillRepository
    {
        public bills Createbill(bills bill);
        public bills getBillById(Guid? id);
        public IEnumerable<bill_items_details> getProductsInBill(Guid? billId);
        public bool deleteBill(Guid? id);
        public bills UpdateBill(Guid? id);
        public bill_items_details getBillItem(Guid productId, Guid billId);
        public void AddProductToBill(bill_items_details billItem);
        public bool RemoveProductFromBill(bill_items_details billItem);
        public bool DeleteItem(Guid id);
        bool UpdateQuantity(Guid billItemId, int quantity);
        IEnumerable<bills> GetBillsList();
        IEnumerable<bill_items_details> GetBillItems(Guid billId);
        Guid GetDefaultPaymentMethodId();
        public void SaveChanges();
    }
}
