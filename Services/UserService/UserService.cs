using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Services.UserService
{
    public class UserService : IUserService
    {
        readonly UserManager<users> _UserManager;
        public UserService(UserManager<users> UserManager)
        {
               _UserManager = UserManager;
        }

        public async Task<users> FindUserByEmail(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email);
        }
        public async Task<users> FindUserByUsername(string Username)
        {
            return await _UserManager.FindByNameAsync(Username);
        }
    }
}
