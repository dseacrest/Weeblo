using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeblo.Models;
using Weeblo.ViewModels;

namespace Weeblo.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<ProjectUser> _signInManager;

        public AuthController(SignInManager<ProjectUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Project", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, 
                                                                            vm.Password,
                                                                            true, false);

                if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                        return RedirectToAction("Project", "App");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                else
                    {
                        ModelState.AddModelError("", "Username or password incorrect");
                    }

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync(); //Gets rid of cookie collection
            }
            return RedirectToAction("Index", "App");
        }

    }
}
