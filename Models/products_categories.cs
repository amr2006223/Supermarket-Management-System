using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_Managment_System.Models
{
    public class products_categories
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("ProductId")]
        public products Product { get; set; }
        [ForeignKey("CategoryId")]
        public categories Category { get; set; }
    }
}
