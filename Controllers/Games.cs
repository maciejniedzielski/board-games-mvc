using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> Index(string name)
        {
            var games = (from g in _dbContext.Game
                         join category in _dbContext.Category on g.CategoryId equals category.Id
                         join publisher in _dbContext.Publisher on g.PublisherId equals publisher.Id
                         select new Game
                         {
                             Id = g.Id,
                             Name = g.Name,
                             Description = g.Description,
                             NoOfPlayers = g.NoOfPlayers,
                             Age = g.Age,
                             AverageGameTime = g.AverageGameTime,
                             Randomness = g.Randomness,
                             Interaction = g.Interaction,
                             Complexity = g.Complexity,
                             Category = category,
                             Publisher = publisher
                         });

            if (!String.IsNullOrEmpty(name))
            {
                games = games.Where(g => g.Name.ToLower().Contains(name.ToLower()));
            }

            TempData["SearchString"] = name ?? "";
            return View(await games.ToListAsync());
        }

        [Route("boardgame/{id}")]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var game = _dbContext.Game.First(g => g.Id == id);
            game.Category = _dbContext.Category.First(category => category.Id == game.CategoryId);
            game.Publisher = _dbContext.Publisher.First(publisher => publisher.Id == game.PublisherId);

            return View(game);
        }

        [Route("games/add")]
        public IActionResult Create()
        {
            
            var categories = _dbContext.Category.ToList();
            ViewData["Categories"] = categories;
                        
            var publishers = _dbContext.Publisher.ToList();
            ViewData["Publishers"] = publishers;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("games/add")]
        public async Task<IActionResult> Create(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(game);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
                ViewData["ErrorMessage"] =
                    "Create failed. Try again.";
            }

            var categories = _dbContext.Category.ToList();
            ViewData["Categories"] = categories;
                    
            var publishers = _dbContext.Publisher.ToList();
            ViewData["Publishers"] = publishers;
            
            return View(game);
        }
        
        [Route("boardgame/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var gameToUpdate = await _dbContext.Game.FirstOrDefaultAsync(s => s.Id == id);
            
            var categories = _dbContext.Category.ToList();
            ViewData["Categories"] = categories;
                    
            var publishers = _dbContext.Publisher.ToList();
            ViewData["Publishers"] = publishers;

            return View(gameToUpdate);
        }
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Route("boardgame/edit/{id}")]
        public async Task<IActionResult> EditGame(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameToUpdate = await _dbContext.Game.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Game>(
                gameToUpdate,
                "",
                g => g.Name,
                g => g.Description,
                g => g.NoOfPlayers,
                g => g.AverageGameTime,
                g => g.Age,
                g => g.Randomness,
                g => g.Interaction,
                g => g.Complexity,
                g => g.CategoryId,
                g => g.PublisherId)
            )
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.ToString());
                    @ViewData["ErrorMessage"] = "Edit failed. Try again.";
                }
            }
            
            var categories = _dbContext.Category.ToList();
            ViewData["Categories"] = categories;
                    
            var publishers = _dbContext.Publisher.ToList();
            ViewData["Publishers"] = publishers;

            return View(gameToUpdate);
        }

        [Route("boardgame/delete/{id}")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _dbContext.Game
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again.";
            }

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("boardgame/delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _dbContext.Game.FindAsync(id);
            if (game == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _dbContext.Game.Remove(game);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new {id = id, saveChangesError = true});
            }
        }
    }
}