using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Services;

using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;


        public ProductController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        //view list of products
        public IActionResult Index()
        {
            IEnumerable<products> products = _productsService.GetProducts();
            return View(products);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            // Retrieve categories from your repository or service
            var categories = _categoriesService.GetCategories();

            // Set ViewBag.Categories to the retrieved categories
            ViewBag.Categories = categories;

            return View();
        }


        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(products product)
        {
            if (ModelState.IsValid)
            {
                bool created = await _productsService.CreateProduct(product);
                if (created)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productFromDb = await _productsService.GetProductById(id.Value);

        if (productFromDb == null)
        {
            return NotFound();
        }

        return View(productFromDb);
    }


         [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(products product)
    {
        if (ModelState.IsValid)
        {
            bool updated = await _productsService.UpdateProduct(product);
            if (updated)
            {
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            // Handle update failure if needed
        }

        return View(product);
    }


         public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productFromDb = await _productsService.GetProductById(id.Value);

        if (productFromDb == null)
        {
            return NotFound();
        }

        return View(productFromDb);
    }

        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        bool deleted = await _productsService.DeleteProduct(id);
        if (deleted)
        {
            TempData["success"] = "Product Deleted Successfully";
        }
        else
        {
            // Handle delete failure if needed
        }

        return RedirectToAction("Index");
    }

    }
}