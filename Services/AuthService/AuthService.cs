using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.AuthService
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<users> _UserManager;
        public AuthService(UserManager<users> UserManager)
        {
            _UserManager = UserManager;
        }
        private users CreateUser(RegisterVM model)
        {
            var NewUser = new users()
            {
                FirstName = model.FirstName,
                UserName = model.Username,
                Email = model.Email,
            };
            return NewUser;
        }
        public async Task<List<IdentityError>> addUser(RegisterVM model)
        {
            // var current_user = await _UserManager.GetUserAsync(User);
            List<IdentityError> errors = new List<IdentityError>();
            users user = CreateUser(model);
            var result = await _UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                //await SignInManager.SignInAsync(user, isPersistent: false);
                if (await assignRole(user, "User"))
                {
                    return null;
                }

                foreach (var error in result.Errors)
                {
                    errors.Add(error);
                }
            }
            return errors;
        }
        private async Task<bool> assignRole(users user, string Role)
        {
            //var role = await _UserManager.AddToRoleAsync(user, Role);
            //return role.Succeeded;
            return true;
        }
    }
}
