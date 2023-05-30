using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private UserManager<users> _UserManager;
        private SignInManager<users> _SignInManager;
        public AuthController(IAuthService AuthService,IUserService UserService, UserManager<users> userManager, SignInManager<users> signInManger)
        {
            _AuthService = AuthService;
            _UserService = UserService;
            _UserManager = userManager;
            _SignInManager = signInManger;
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> login()
        {
            var user = await _UserManager.GetUserAsync(User);
            if (_SignInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Manager")) return RedirectToAction("Index", "Home");
                if (User.IsInRole("Cashier")) return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(loginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
