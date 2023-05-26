using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Data;
using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.Services.AuthService;
using Supermarket_Managment_System.Services.UserService;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _AuthService;
        private IUserService _UserService;
        public AuthController(IAuthService AuthService,IUserService UserService)
        {
            _AuthService = AuthService;
            _UserService = UserService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet("User/Register")]
        public IActionResult UserRegisteration()
        {            
            return View();
        }

        [HttpPost("User/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegisteration(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                users User = await _UserService.FindUserByUsername(model.Username);
                if (User != null)
                {
                    ModelState.AddModelError("Username", "Username already in use");
                    return View();
                }
                List<IdentityError> errors = await this._AuthService.addUser(model);

                if(errors == null)
                {
                    return View("/Home/Index");
                }
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
