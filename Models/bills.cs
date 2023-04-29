using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_Managment_System.Models
{
    public class bills
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public float TotalPrice { get; set; }
        [ForeignKey("UserId")]
        public users User { get; set; }
        [ForeignKey("PaymentMethodId")]
        public payments Payment { get; set; }
    }
}
