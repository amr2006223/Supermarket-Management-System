using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_Managment_System.Models
{
    public class users
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public roles Role { get; set; }
    }
}
