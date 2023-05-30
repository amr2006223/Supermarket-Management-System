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




    }
}
