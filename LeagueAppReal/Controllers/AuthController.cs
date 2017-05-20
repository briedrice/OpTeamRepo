using LeagueAppReal.Models;
using LeagueAppReal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueAppReal.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;

        public AuthController(SignInManager<User> signInManager) {
            _signInManager = signInManager;
        }

        public IActionResult Login() {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Profile", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl) {
            if (ModelState.IsValid) {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl)) {
                        return RedirectToAction("Profile", "Home");
                    }
                    return RedirectToAction(returnUrl);
                }
                else {
                    ModelState.AddModelError("", "Username or Password Incorrect");
                }
                    
            }
            return View();
        }

        public async Task<ActionResult> Logout() {
            if (User.Identity.IsAuthenticated) {
                await _signInManager.SignOutAsync();
            
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
