﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;

namespace ThesisOct2023.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Register() => View();


        public async Task<IActionResult> Login() => View();


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserRole = model.Role

                    // Set other custom properties as needed
                };

                var result = await _userManager.CreateAsync(user, model.Password);
               
                if (result.Succeeded)
                {
                    // User registration successful
                    // You can sign the user in if needed
                    await _userManager.AddToRoleAsync(user, model.Role);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser signeduser = await _userManager.FindByEmailAsync(model.Email);

                if (signeduser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(signeduser.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    if (result.RequiresTwoFactor)
                    {
                        // Handle two-factor authentication
                    }

                    if (result.IsLockedOut)
                    {
                        // Handle locked-out user
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
                else
                {
                    // Handle the case where the email doesn't exist
                    ModelState.AddModelError(string.Empty, "Email not found. Please check your credentials.");
                }
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
