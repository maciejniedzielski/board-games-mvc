using System.Threading.Tasks;
using BoardGames.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        [Route("auth/register")]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("auth/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        
        [Route("auth/login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("auth/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
        
        
        [Route("auth/add-role")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAction()
        {
            IdentityResult roleResult;
            
            var roleCheck = await _roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                
                roleResult = await _roleManager.CreateAsync(role);
            }
            
            return RedirectToAction("index", "Home");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRoleAction()
        {

            var user = await _userManager.FindByIdAsync("df2bbc3f-5e63-49cb-94b4-32e39305d549");
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            
            return RedirectToAction("index", "Home");
        }
    }
}