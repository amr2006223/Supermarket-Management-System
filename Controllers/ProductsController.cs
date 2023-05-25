using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //view list of products
        public IActionResult Index()
        {
            IEnumerable<products> objType = _dbContext.product; // Assign the retrieved data to a variable

            return View(objType);
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

        public IActionResult Edit(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var productFromDb = _dbContext.product.Find(Id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(products obj)
{
    _dbContext.Attach(obj);
    _dbContext.Entry(obj).State = EntityState.Modified;
    _dbContext.SaveChanges();
    TempData["success"] = "Product Updated Successfully";
    return RedirectToAction("Index");
}


          public IActionResult Delete(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var AccountFromDb = _dbContext.product.Find(Id);

            if (AccountFromDb == null)
            {
                return NotFound();
            }
            return View(AccountFromDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var obj = _dbContext.product.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.product.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Account Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}