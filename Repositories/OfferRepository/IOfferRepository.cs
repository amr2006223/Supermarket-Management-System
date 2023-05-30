using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Repositories.OfferRepository
{
    public interface IOfferRepository
    {
        Task<IEnumerable<offers>> GetAllOffers();
        Task<offers> GetOfferById(Guid id);
        Task CreateOffer(offers offer);
        Task UpdateOffer(offers offer);
        Task DeleteOffer(Guid id);
        bool OfferExists(Guid id);
    }
}
