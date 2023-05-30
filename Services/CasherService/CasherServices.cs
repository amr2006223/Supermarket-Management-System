using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.CasherService
{
    public class CasherServices : ICasherServices
    {
        private readonly db_context _context;
        private readonly UserManager<users> _UserManager;
        public CasherServices(db_context context, UserManager<users> UserManager)
        {
            _context = context;
            _UserManager = UserManager;
        }

        public async Task<users> Create(RegisterVM model)
        {
            users newUser = new users()
            {

                FirstName = model.FirstName,
                UserName = model.Username,
                Email = model.Email,

            };
            var result = await _UserManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {

                if (await assignRole(newUser, "Cashier"))
                {
                    return null;
                }


                return newUser;
            }
            return null;
        }
        private async Task<bool> assignRole(users user, string Role)
        {
            var role = await _UserManager.AddToRoleAsync(user, Role);
            return role.Succeeded;
            return true;
        }


        public async Task<users> Update(users u)
        {

            var user = await _UserManager.FindByIdAsync(u.Id);
            if (user == null)
            {
                return null;
            }

            user.FirstName = u.FirstName;
            user.UserName = u.UserName;
            user.Email = u.Email;

            var result = await _UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<users> Delete(users u)
        {
            var user = await _UserManager.FindByIdAsync(u.Id);
            if (user == null)
            {
                return null;
            }
            var result = await _UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }
        public async Task<IEnumerable<users>> Index()
        {
            var users = await _UserManager.GetUsersInRoleAsync("Cashier");
            return users;
        }

        public void test()
        {
            throw new NotImplementedException();
        }
    }
}  
    

