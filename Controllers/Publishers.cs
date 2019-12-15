using System.Linq;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Publishers : Controller
    {
        
        private readonly AppDbContext _dbContext;
        
        public Publishers(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("publishers")]
        public IActionResult Index()
        {
            var publishers = _dbContext.Publisher.ToList();
            return View(publishers);
        }
    }
}