using System.Linq;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Categories : Controller
    {
        
        private readonly AppDbContext _dbContext;

        public Categories(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        [Route("categories")]
        public IActionResult Index()
        {
            var categories = _dbContext.Category.ToList();
            return View(categories);
        }
    }
}