using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Games : Controller
    {
        
        private readonly AppDbContext _dbContext;
        
        public Games(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("games")]
        public IActionResult Index()
        {
            var games = _dbContext.Game.ToList();
            foreach (var game in games)
            {
                game.Category = _dbContext.Category.First(category => category.Id == game.CategoryId);
                game.Publisher = _dbContext.Publisher.First(publisher => publisher.Id == game.PublisherId);
            }
            
            return View(games);
        }
    }
}