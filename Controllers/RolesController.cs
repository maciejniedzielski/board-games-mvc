using System.Threading.Tasks;
using BoardGames.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoardGames.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        [Route("/admin/roles")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [Route("/admin/roles/create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Route("/admin/roles/create")]
        public async Task<IActionResult> Create(RoleCreateViewModel role)
        {
            if (ModelState.IsValid)
            {
                var roleCheck = await _roleManager.RoleExistsAsync(role.Name);

                if (!roleCheck)
                {
                    IdentityRole newRole = new IdentityRole
                    {
                        Name = role.Name
                    };
                
                    IdentityResult roleResult = await _roleManager.CreateAsync(newRole);
                    return RedirectToAction("index", "Roles");
                }
                else
                {
                    ModelState.AddModelError("Name", "Role with given name already exists.");
                }
            }
            
            return View(role);
        }
    }
}