using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BoardGames.ViewModels
{
    public class UsersIndexViewModel
    {
        public IQueryable<IdentityUser> Users { get; set; }
        public IQueryable<IdentityRole> Roles { get; set; }
        
        public UsersIndexViewModel(IQueryable<IdentityUser> users, IQueryable<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }
    }
}