using System.ComponentModel.DataAnnotations;

namespace Supermarket_Managment_System.Models
{
    public class payments
    {
        [Key]
        public Guid Id { get; set; }
        public string MethodName { get; set; }
    }
}
