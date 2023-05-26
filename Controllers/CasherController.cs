using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Services;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Controllers
{
    public class CasherController : Controller
    {
        private readonly db_context _db;
        //private readonly CasherServices _casherServices;

        public CasherController(db_context db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBill()
        {
            IEnumerable<products> products = _db.product;
            bills bill = new bills();
            bill.Id = new Guid();
            bill.UserId = new Guid("902B72D0-42B2-4CD5-970A-08DB5CD4D842");
            bill.PaymentMethodId = new Guid("A7EE157E-AC7F-4F51-774A-08DB5CD6635E");
            bill.TotalPrice = 0;
            _db.bill.Add(bill);
            _db.SaveChanges();
            return View("CreateBill", (products, bill));
        }

        [HttpPost]
        public IActionResult addProductToBill(Guid product_id, Guid bill_id, int quantity)
        {
            var existingItem = _db.bill_items_details.FirstOrDefault(b => b.ProductId == product_id && b.BillId == bill_id);
            if (existingItem != null)
            {
                return Json("Product already exists in the bill.");
            }

            bill_items_details bill_items_details = new bill_items_details();
            bill_items_details.Id = new Guid();
            bill_items_details.BillId = bill_id;
            bill_items_details.ProductId = product_id;
            bill_items_details.Quantity = quantity;
            _db.bill_items_details.Add(bill_items_details);
            bills bills = _db.bill.Find(bill_id);
            products products = _db.product.Find(product_id);
            var offer = _db.products_offers.FirstOrDefault(po => po.ProductId == product_id);
            if (offer != null)
            {
                // Apply the offer
                float offerPrice = products.Price * offer.Offer.Discount;
                bills.TotalPrice += offerPrice * quantity;
            }
            else
            {
                bills.TotalPrice += products.Price * quantity;
            }
            _db.SaveChanges();
            Console.WriteLine("Product added to bill");

            return Json("Product added to the bill successfully.");
        }


        public IActionResult GetProductsInBill(Guid bill_id)
        {
            var productsInBill = _db.bill_items_details
                .Where(b => b.BillId == bill_id)
                .Include(b => b.Product)
                .ToList();
            var productsWithQuantity = productsInBill.Select(b => new
            {
                id = b.Product.Id,
                name = b.Product.Name,
                price = b.Product.Price,
                quantity = b.Quantity
            });

            return Json(productsWithQuantity);
        }


        [HttpPost]
        public IActionResult DeleteProductFromBill(Guid product_id, Guid bill_id)
        {
            var billItem = _db.bill_items_details.FirstOrDefault(b => b.ProductId == product_id && b.BillId == bill_id);
            if (billItem != null)
            {
                _db.bill_items_details.Remove(billItem);
                bills bills = _db.bill.Find(bill_id);
                products products = _db.product.Find(product_id);
                bills.TotalPrice -= products.Price * billItem.Quantity;
                _db.SaveChanges();

                return Json("Product removed from the bill successfully.");
            }
            else
            {
                return Json("Product not found in the bill.");
            }
        }

        [HttpPost]
        public IActionResult EditProductQuantity(Guid product_id, Guid bill_id, int quantity)
        {
            var billItem = _db.bill_items_details.FirstOrDefault(b => b.ProductId == product_id && b.BillId == bill_id);
            if (billItem != null)
            {
                bills bills = _db.bill.Find(bill_id);
                products products = _db.product.Find(product_id);

                bills.TotalPrice -= products.Price * billItem.Quantity;
                bills.TotalPrice += products.Price * quantity;

                billItem.Quantity = quantity;
                _db.SaveChanges();

                return Json("Quantity updated successfully.");
            }
            else
            {
                return Json("Product not found in the bill.");
            }
        }

    }
}
