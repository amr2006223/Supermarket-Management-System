using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using System;

namespace Supermarket_Managment_System.Repositories
{
    public class CasherRepository : ICasherRepository
    {
        private readonly db_context _db;

        public CasherRepository(db_context db)
        {
            _db = db;
        }

        public IEnumerable<products> GetAllProducts()
        {
            return _db.product.ToList();
        }

        public IEnumerable<categories> GetAllCategories()
        {
            return _db.category.ToList();
        }

        public products_categories GetProductCategory(Guid productId)
        {
            return _db.product_catoegories
                .Include(pc => pc.Category)
                .FirstOrDefault(pc => pc.ProductId == productId);
        }

        public Guid GetDefaultPaymentMethodId()
        {
            return _db.payment.FirstOrDefault().Id;
        }

        public void CreateBill(bills bill)
        {
            _db.bill.Add(bill);
            _db.SaveChanges();
        }

        public bill_items_details GetBillItem(Guid productId, Guid billId)
        {
            return _db.bill_items_details
                .FirstOrDefault(b => b.ProductId == productId && b.BillId == billId);
        }

        public bills GetBill(Guid billId)
        {
            return _db.bill.FirstOrDefault(b => b.Id == billId);
        }

        public products GetProduct(Guid productId)
        {
            return _db.product.Find(productId);
        }

        public products_offers GetProductOffer(Guid productId)
        {
            return _db.products_offers.FirstOrDefault(po => po.ProductId == productId);
        }

        public void AddProductToBill(bill_items_details billItem)
        {
            _db.bill_items_details.Add(billItem);
        }

        public void RemoveProductFromBill(bill_items_details billItem)
        {
            _db.bill_items_details.Remove(billItem);
        }

        public IEnumerable<bill_items_details> GetProductsInBill(Guid billId)
        {
            return _db.bill_items_details
                .Where(b => b.BillId == billId)
                .Include(b => b.Product)
                .ToList();
        }

        public float GetTotalPrice(Guid billId)
        {
            float totalPrice = _db.bill.Find(billId).TotalPrice;
            return totalPrice;
        }

        public IEnumerable<payments> GetAllPaymentMethods()
        {
            return _db.payment.ToList();
        }

        public IQueryable<bill_items_details> GetBillItemDetailsByBillId(Guid billId)
        {

            return _db.bill_items_details.Where(bid => bid.BillId == billId);
        }

        public bool HasOfferForProduct(Guid productId)
        {
            return _db.products_offers.Any(po => po.ProductId == productId);
        }

        public float GetDiscountForProduct(Guid productId)
        {
            var productOffer = _db.products_offers.FirstOrDefault(po => po.ProductId == productId);
            if (productOffer != null)
            {
                var offer = _db.offers.FirstOrDefault(o => o.Id == productOffer.OfferId);

                return offer.Discount;
            }
            return 0;

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
