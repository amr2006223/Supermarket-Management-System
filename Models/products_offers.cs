using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_Managment_System.Models
{
    public class products_offers
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OfferId { get; set; }
        [ForeignKey("ProductId")]
        public products Product { get; set; }
        [ForeignKey("OfferId")]
        public offers Offer { get; set; }
    }
}
