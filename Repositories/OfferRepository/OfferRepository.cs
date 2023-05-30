using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories.OfferRepository
{
    public class OfferRepository : IOfferRepository
    {
        private readonly db_context _db;
        public OfferRepository(db_context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<offers>> GetAllOffers()
        {
            return await _db.offers.ToListAsync();
        }

        public async Task<offers> GetOfferById(Guid id)
        {
            return await _db.offers.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateOffer(offers offer)
        {
            offer.Id = Guid.NewGuid();
            _db.Add(offer);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOffer(offers offer)
        {
            _db.Update(offer);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOffer(Guid id)
        {
            var offer = await _db.offers.FindAsync(id);
            if (offer != null)
            {
                _db.offers.Remove(offer);
                await _db.SaveChangesAsync();
            }
        }

        public bool OfferExists(Guid id)
        {
            return _db.offers.Any(e => e.Id == id);
        }
    }
}
