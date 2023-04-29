using System.ComponentModel.DataAnnotations;

namespace Supermarket_Managment_System.Models
{
    public class offers
    {
        [Key]
        public Guid Id{ get; set; }
        public string Name { get; set; }
    }
}
