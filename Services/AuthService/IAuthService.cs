using Microsoft.AspNetCore.Identity;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.AuthService
{
    public interface IAuthService
    {
        public Task<List<IdentityError>> addUser(RegisterVM model);
    }
}
