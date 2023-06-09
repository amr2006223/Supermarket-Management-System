﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;
using Supermarket_Managment_System.Services.CasherService;
using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.Services.BillService;
using Supermarket_Managment_System.Repositories;
using Supermarket_Managment_System.Services;

namespace Supermarket_Managment_System.Controllers
{
    public class CasherController : Controller
    {
        private ICasherService _casherService;
        private IProductsService _productService;
        private ICategoriesService _categoryService;
        private readonly UserManager<users> _userManager;

        public CasherController (ICasherService casherServices, UserManager<users> userManager, IProductsService productService, ICategoriesService categoryService)
        {
            _casherService = casherServices;
            _userManager = userManager;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Casher/CreateBill")]

        public async Task<IActionResult> CreateBill()
        {
            List<ProductsToBillVM> productsToBillVM = _casherService.GetProductsWithCategories().ToList();
            IEnumerable<categories> categories = _categoryService.GetCategories();
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



        //gets the bills list /Casher/BillsList
        public IActionResult BillsList()
        {
            var billsList = _casherService.GetBillsList(); // Call the method to get the bills list
            return View(billsList);
        }

        //delete bills
        public IActionResult Delete(Guid id)
        {
            try
            {
                _casherService.DeleteBill(id);
                return RedirectToAction("BillsList");
            }
            catch (Exception.NotFoundException)
            {
                return NotFound();
            }
        }

        public IActionResult BillItems(Guid id)
        {
            var billItems = _casherService.GetBillItems(id);
            return View(billItems);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(Guid billItemId, int quantity)
        {
            bool success = _casherService.UpdateQuantity(billItemId, quantity);
            if (!success)
            {
                return NotFound();
            }

            // Redirect back to the BillItems view
            return RedirectToAction("BillItems", new { id = billItemId });
        }

        public IActionResult DeleteItem(Guid id)
        {
            bool success = _casherService.DeleteItem(id);
            if (!success)
            {
                return NotFound();
            }

            // Redirect back to the BillItems view
            return RedirectToAction("BillItems", new { id });
        }

    }
}
