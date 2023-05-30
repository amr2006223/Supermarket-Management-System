using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;
using Supermarket_Managment_System.Services.CasherService;
using Microsoft.AspNetCore.Identity;

namespace Supermarket_Managment_System.Controllers
{
    public class CasherController : Controller
    {
        private ICasherService _casherService;
        private readonly UserManager<users> _userManager;

        public CasherController (ICasherService casherServices,UserManager<users> userManager)
        {
            _casherService = casherServices;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Casher/CreateBill")]

        public async Task<IActionResult> CreateBill()
        {
            List<ProductsToBillVM> productsToBillVM = _casherService.GetProductsWithCategories().ToList();
            IEnumerable<categories> categories = _casherService.GetAllCategories();
            bills bill = new bills();
            users LoggedInUser = await _userManager.GetUserAsync(User);
            bill.UserId = LoggedInUser.Id;
            bill.PaymentMethodId = _casherService.GetDefaultPaymentMethodId();
            bill.TotalPrice = 0;
            _casherService.CreateBill(bill);
            return View((productsToBillVM, bill, categories));
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
