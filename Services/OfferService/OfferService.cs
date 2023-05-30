using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Repositories.OffersRepositorie;

namespace Supermarket_Managment_System.Services.OfferService
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepositoriy;
        public OfferService(IOfferRepository offerRepositorie)
        {
            _offerRepositoriy = offerRepositorie;
        }

        public async Task<IEnumerable<offers>> GetAllOffers()
        {
            return await _offerRepositoriy.GetAllOffers();
        }

        public async Task<offers> GetOfferById(Guid id)
        {
            return await _offerRepositoriy.GetOfferById(id);
        }

        public async Task CreateOffer(offers offer)
        {
            await _offerRepositoriy.CreateOffer(offer);
        }

        public async Task UpdateOffer(offers offer)
        {
            await _offerRepositoriy.UpdateOffer(offer);
        }

        public async Task DeleteOffer(Guid id)
        {
            await _offerRepositoriy.DeleteOffer(id);
        }

        public bool OfferExists(Guid id)
        {
            return _offerRepositoriy.OfferExists(id);
        }
    }
}
