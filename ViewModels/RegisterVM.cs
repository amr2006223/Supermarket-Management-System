using System.ComponentModel.DataAnnotations;

namespace Supermarket_Managment_System.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,}(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid name. Please enter a name that contains at least three alphabetical characters and may include spaces, hyphens, apostrophes, commas, or periods.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[cC][oO][mM]$", ErrorMessage= "Invalid email address. Please enter a valid email address that contains an \"@\" symbol and ends with .com ")]
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
    }
}
