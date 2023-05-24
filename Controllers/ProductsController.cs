using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

// Update the namespace and class name as per your project's conventions
namespace Supermarket_Managment_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly db_context _dbContext; // Replace YourDbContext with the actual name of your database context class

        public ProductController(db_context dbContext) // Replace YourDbContext with the actual name of your database context class
        {
            _dbContext = dbContext;
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(products product) // Replace 'products' with the actual name of your model class
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                _dbContext.product.Add(product); // Replace 'products' with the actual name of your DbSet property

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // Replace "Index" and "Home" with your desired action and controller
            }

            return View(product);
        }
    }
}