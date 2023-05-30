using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Models;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly db_context _dbContext;

        public ProductsRepository(db_context dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<products> GetProducts()
    {
        return _dbContext.product.ToList();
    }
    public async Task<bool> CreateProduct(products product)
    {
        try
        {
            product.Id = Guid.NewGuid();
            _dbContext.product.Add(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception.NotFoundException)
        {
            
            return false;
        }
    }
    public async Task<products> GetProductById(Guid id)
    {
        return await _dbContext.product.FindAsync(id);
    }
     public async Task<bool> UpdateProduct(products product)
    {
        try
        {
            _dbContext.Attach(product);
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception.NotFoundException)
        {
            return false;
        }
    }
     public async Task<bool> DeleteProduct(Guid? id)
    {
        try
        {
            var product = await _dbContext.product.FindAsync(id);
            if (product == null)
                return false;

            _dbContext.product.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception.NotFoundException)
        {
            return false;
        }
    }
    }
}