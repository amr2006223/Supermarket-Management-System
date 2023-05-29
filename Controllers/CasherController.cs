using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;
using Supermarket_Managment_System.Services.CasherService;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Supermarket_Managment_System.Controllers
{
    public class CasherController : Controller
    {
        private readonly db_context _db;
        private ICasherServices _casherServices;

        public CasherController(db_context db, ICasherServices casherServices)
        {
            _db = db;
            _casherServices = casherServices;

            //products_categories produc_categorie = new products_categories();
            //produc_categorie.ProductId = new Guid("3a553de8-1169-4ec7-98dc-08db5d2d5e50");
            //produc_categorie.CategoryId = new Guid("21b6c610-fb22-4389-e6aa-08db605584ac");
            //_db.product_catoegories.Add(produc_categorie);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBill()
        {
            List<ProductsToBillVM> productsToBillVM = new List<ProductsToBillVM>();
            //get all products
            IEnumerable<products> products = _db.product.ToList();
            //get all categories
            IEnumerable<categories> categories = _db.category;
            //get products categories
            foreach (var product in products)
            {
                var productCategory = _db.product_catoegories
                                         .Include(pc => pc.Category)
                                         .FirstOrDefault(pc => pc.ProductId == product.Id);

                if (productCategory != null)
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                        category = productCategory.Category
                    };
                    productsToBillVM.Add(productsToBill);
                }
                else
                {
                    ProductsToBillVM productsToBill = new ProductsToBillVM()
                    {
                        product = product,
                    };
                    productsToBillVM.Add(productsToBill);
                }
            }
            //create new bill
            bills bill = new bills();

            //set bill user id to the current user id
            bill.UserId = "f2f3f1f3-f243-44d0-a2df-18ef6f558925";
            //set bill payment method to first payment method
            bill.PaymentMethodId = _db.payment.FirstOrDefault().Id;

            bill.TotalPrice = 0;
            _db.bill.Add(bill);
            _db.SaveChanges();
            return View("CreateBill", (productsToBillVM, bill, categories));
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

            if (bill_id != null)
            {
                var bill = _db.bill.FirstOrDefault(b => b.Id == bill_id);
                products products = _db.product.Find(product_id);
                var offer = _db.products_offers.FirstOrDefault(po => po.ProductId == product_id);
                if (offer != null)
                {
                    // Apply the offer
                    float offerPrice = products.Price * offer.Offer.Discount;
                    bill.TotalPrice += offerPrice * quantity;
                }
                else
                {
                    bill.TotalPrice += products.Price * quantity;
                }
                _db.SaveChanges();
                Console.WriteLine("Product added to bill");

                return Json("Product added to the bill successfully.");
            }
            else
            {
                return Json("Bill ID is null.");
            }
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
