using System.ComponentModel.DataAnnotations;

namespace Supermarket_Managment_System.Models
{
    public class roles
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
