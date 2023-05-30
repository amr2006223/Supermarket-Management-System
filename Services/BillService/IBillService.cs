using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Services.BillService
{
    public interface IBillService
    {
        public Guid GetDefaultPaymentMethodId();
        public void CreateBill(bills bill);
        public Task<string> AddProductToBill(Guid productId, Guid billId, int quantity);
        public IEnumerable<object> GetProductsInBill(Guid billId);
        public  Task<string> DeleteProductFromBill(Guid productId, Guid billId);
        public  Task<string> EditProductQuantity(Guid productId, Guid billId, int quantity);
        public IEnumerable<bills> GetBillsList();
        public void DeleteBill(Guid id);
        public IEnumerable<bill_items_details> GetBillItems(Guid billId);
        public bool UpdateQuantity(Guid billItemId, int quantity);
        public bool DeleteItem(Guid id);
    }
}
