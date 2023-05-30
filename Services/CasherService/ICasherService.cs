using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.CasherService
{
    public interface ICasherService
    {
        IEnumerable<ProductsToBillVM> GetProductsWithCategories();
        Guid GetDefaultPaymentMethodId();
        void CreateBill(bills bill);
        Task<string> AddProductToBill(Guid productId, Guid billId, int quantity);
        IEnumerable<object> GetProductsInBill(Guid billId);
        Task<string> DeleteProductFromBill(Guid productId, Guid billId);
        Task<string>EditProductQuantity(Guid productId, Guid billId, int quantity);
        IEnumerable<categories> GetAllCategories();
        IEnumerable<bills> GetBillsList();
        void DeleteBill(Guid id);
        IEnumerable<bill_items_details> GetBillItems(Guid billId);
        bool UpdateQuantity(Guid billItemId, int quantity);
        bool DeleteItem(Guid id);
    }
}
