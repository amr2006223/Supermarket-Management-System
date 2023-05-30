using Supermarket_Managment_System.Models;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Data
{
    public interface IProductsRepository
    {
            IEnumerable<products> GetProducts();
            Task<bool> CreateProduct(products product);
            Task<products> GetProductById(Guid id);
            Task<bool> UpdateProduct(products product);
            Task<bool> DeleteProduct(Guid? id);
            products_categories GetProductCategory(Guid productId);
            IEnumerable<bill_items_details> GetProductsInBill(Guid billId);
            products_offers GetProductOffer(Guid productId);
            void SaveChanges();
    }
}
