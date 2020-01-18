using System.Linq;
using System.Threading.Tasks;
using BoardGames.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class UsersController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        [Route("/admin/users")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _userManager.Users;
            var roles = _roleManager.Roles;
            
            UsersIndexViewModel viewModel = new UsersIndexViewModel(users, roles);
            
            return View(viewModel);
        }
        
        [HttpPost]
        [Route("/admin/add-user-to-role")]
        public async Task<IActionResult> AddUserToRoleAction([FromBody]Payload payload)
        {
            var user = await _userManager.FindByIdAsync(payload.UserId);
            
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
            
            var result = await _userManager.AddToRoleAsync(user, payload.RoleName);

            if (result.Succeeded)
            {
                return Ok(new { status = true, msg = "User successfully added to role" });
            }
                return BadRequest(new { status = false, msg = "An error has occurred", error = result.Errors});
            
        }
    }
    
    public class Payload
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}

