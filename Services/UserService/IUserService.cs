using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.Services.UserService
{
    public interface IUserService
    {
        Task<users> FindUserByEmail(string Email);
        Task<users> FindUserByUsername(string Username);
    }
}
