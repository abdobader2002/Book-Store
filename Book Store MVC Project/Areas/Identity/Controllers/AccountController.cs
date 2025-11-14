using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store_MVC_Project.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUserAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = new ApplicationUser
                {
                    Name = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.UserName
                };

                var result = await userManager.CreateAsync(appUser, registerViewModel.Password);

                if (result.Succeeded)
                {
                    // assign to Customer role by default
                    await userManager.AddToRoleAsync(appUser, "Customer");

                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View("Register", registerViewModel);
        }
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel userViewModel)
        {
            if (ModelState.IsValid == true)
            {
                //check found 
                ApplicationUser appUser =
                    await userManager.FindByNameAsync(userViewModel.UserName);
                if (appUser != null)
                {
                    bool found =
                         await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
                    if (found == true)
                    {
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim("UserAddress", appUser.Email));

                        await signInManager.SignInWithClaimsAsync(appUser, userViewModel.StayLogged, Claims);
                        //await signInManager.SignInAsync(appUser, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }

                }
                ModelState.AddModelError("", "Username OR PAssword wrong");
                //create cookie
            }
            return View("Login", userViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}
    

