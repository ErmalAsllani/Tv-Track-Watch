using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TV_Track.ViewModels;

namespace TV_Track.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel accountViewModel)
        {

            var user = await _userManager.FindByNameAsync(accountViewModel.Username);

            if(user != null)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(accountViewModel.Username, accountViewModel.Password, accountViewModel.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel accountViewModel)
        {
            var user = new IdentityUser()
            {
                UserName = accountViewModel.Username,
                Email = accountViewModel.Email
            };

            var userCreation = await _userManager.CreateAsync(user, accountViewModel.Password);

            if (userCreation.Succeeded)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user, accountViewModel.Password, accountViewModel.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(accountViewModel);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
