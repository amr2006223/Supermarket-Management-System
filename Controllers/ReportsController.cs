using Microsoft.AspNetCore.Mvc;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Controllers
{
    public class ReportsController : Controller
    {
        private readonly db_context _context;
        public ReportsController(db_context context)
        {
            _context = context;
        }
        [HttpGet("/show")]
        public IActionResult Reports()
        {
            List<products> products = _context.product.ToList();
            Dictionary<string, float> dict = new Dictionary<string, float>();
            foreach (products product in products)
            {
                dict.Add(product.Name, product.Price);
            }
            return View(dict);
        }
        public IActionResult ProductBillReport()
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            List<bill_items_details> bill_items_details = _context.bill_items_details.ToList();
            foreach(bill_items_details billDetail in bill_items_details)
            {
                products products = _context.product.Where(x => x.Id == billDetail.ProductId).FirstOrDefault();
                if (products == null) continue;
                if (!dict.ContainsKey(products.Name)) dict.Add(products.Name, billDetail.Quantity);
                else dict[products.Name] = dict[products.Name] + billDetail.Quantity;
            }
            return View(dict);

        }
    }
}
