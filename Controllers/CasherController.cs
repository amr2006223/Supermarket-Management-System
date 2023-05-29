using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;
using Supermarket_Managment_System.Services.CasherService;

namespace Supermarket_Managment_System.Controllers
{
    public class CasherController : Controller
    {
        private ICasherService _casherService;

        public CasherController(db_context db, ICasherService casherServices)
        {
            _casherService = casherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBill()
        {
            List<ProductsToBillVM> productsToBillVM = _casherService.GetProductsWithCategories().ToList();
            IEnumerable<categories> categories = _casherService.GetAllCategories();
            bills bill = new bills();
            bill.UserId = "f2f3f1f3-f243-44d0-a2df-18ef6f558925";
            bill.PaymentMethodId = _casherService.GetDefaultPaymentMethodId();
            bill.TotalPrice = 0;
            _casherService.CreateBill(bill);
            return View("CreateBill", (productsToBillVM, bill, categories));
        }

        [HttpPost]
        public IActionResult addProductToBill(Guid product_id, Guid bill_id, int quantity)
        {
            var result = _casherService.AddProductToBill(product_id, bill_id, quantity);
            return Json(result);
        }

        public IActionResult GetProductsInBill(Guid bill_id)
        {
            var productsInBill = _casherService.GetProductsInBill(bill_id);
            return Json(productsInBill);
        }


        [HttpPost]
        public IActionResult DeleteProductFromBill(Guid product_id, Guid bill_id)
        {
            var result = _casherService.DeleteProductFromBill(product_id, bill_id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult EditProductQuantity(Guid product_id, Guid bill_id, int quantity)
        {
            var result = _casherService.EditProductQuantity(product_id, bill_id, quantity);
            return Json(result);
        }


    }
}
