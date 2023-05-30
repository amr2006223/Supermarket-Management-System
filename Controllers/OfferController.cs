using Microsoft.AspNetCore.Mvc;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Services.OfferService;

namespace Supermarket_Managment_System.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offersService;

        public OfferController(IOfferService offersService)
        {
            _offersService = offersService;
        }

        // GET: Offers
        public async Task<IActionResult> Index()
        {
            var offers = await _offersService.GetAllOffers();
            return View(offers);
        }

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || !_offersService.OfferExists(id.Value))
            {
                return NotFound();
            }

            var offer = await _offersService.GetOfferById(id.Value);
            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Discount")] offers offer)
        {
            if (ModelState.IsValid)
            {
                await _offersService.CreateOffer(offer);
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !_offersService.OfferExists(id.Value))
            {
                return NotFound();
            }

            var offer = await _offersService.GetOfferById(id.Value);
            return View(offer);
        }

        // POST: Offers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Discount")] offers offer)
        {
            if (id != offer.Id || !_offersService.OfferExists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _offersService.UpdateOffer(offer);
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || !_offersService.OfferExists(id.Value))
            {
                return NotFound();
            }

            var offer = await _offersService.GetOfferById(id.Value);
            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!_offersService.OfferExists(id))
            {
                return NotFound();
            }

            await _offersService.DeleteOffer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
