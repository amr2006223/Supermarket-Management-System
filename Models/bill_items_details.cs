using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_Managment_System.Models
{
    public class bill_items_details
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId{ get; set; }
        public Guid BillId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ProductId")]
        public products Product { get; set; }
        [ForeignKey("BillId")]
        public bills Bill { get; set; }
    }
}
