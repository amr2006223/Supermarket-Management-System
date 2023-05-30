using System.ComponentModel.DataAnnotations;

namespace Supermarket_Managment_System.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]{3,20}\d{2,5}$", ErrorMessage = "Enter A Valid UserName(Begin with atleast 3 letters then add 2 to 5 numbers)")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must be (min 8 characters, atleast one uppercase,one lowercase and one number)")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
       // public string Role { get; set; }
    }
}
