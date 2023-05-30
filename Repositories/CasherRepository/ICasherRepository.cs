using Supermarket_Managment_System.Models;
using System;
using System.Collections.Generic;


namespace Supermarket_Managment_System.Repositories



{
    public interface ICasherRepository
    {
        IEnumerable<products> GetAllProducts();
        IEnumerable<categories> GetAllCategories();
        products_categories GetProductCategory(Guid productId);
        Guid GetDefaultPaymentMethodId();
        void CreateBill(bills bill);
        bill_items_details GetBillItem(Guid productId, Guid billId);
        bills GetBill(Guid billId);
        products GetProduct(Guid productId);
        products_offers GetProductOffer(Guid productId);
        void AddProductToBill(bill_items_details billItem);
        void RemoveProductFromBill(bill_items_details billItem);
        IEnumerable<bill_items_details> GetProductsInBill(Guid billId);
        void SaveChanges();
        IEnumerable<bills> GetBillsList();
        bills GetBillById(Guid id);
        void DeleteBill(bills bill);
        IEnumerable<bill_items_details> GetBillItems(Guid billId);
        bool UpdateQuantity(Guid billItemId, int quantity);
        bool DeleteItem(Guid id);
    }
}



