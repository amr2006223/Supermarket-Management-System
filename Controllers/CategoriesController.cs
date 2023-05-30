using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Services;

using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;


        public CategoriesController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(categories category)
        {
            if (ModelState.IsValid)
            {
                bool success = _categoriesService.AddCategory(category);
                if (success)
                {
                    TempData["SuccessMessage"] = "Category added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add the category.");
                }
            }
            return View(category);
        }

    }
}