using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;
using BoardGames.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(string name)
        {
            var publishers = _dbContext.Publisher.AsQueryable();
            
            if (!String.IsNullOrEmpty(name))
            {
                publishers = publishers.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            TempData["SearchString"] = name ?? "";
            return View(await publishers.ToListAsync());
        }
        
        [Route("publisher/{id}")]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var publisher = _dbContext.Publisher.First(p => p.Id == id);
            var publisherGames = _dbContext.Game.Where(g => g.PublisherId == id).ToList();
            
            foreach (var game in publisherGames)
            {
                game.Category = _dbContext.Category.First(c => c.Id == game.CategoryId);
                game.Publisher = _dbContext.Publisher.First(p => p.Id == game.PublisherId);
            }
            
            PublisherDetailViewModel categoryViewModel = new PublisherDetailViewModel(publisher);
            categoryViewModel.GamesTableViewModel = new GamesTableViewModel(publisherGames, "List of games for selected publisher", false);
            
            return View(categoryViewModel);
        }

        [Route("publisher/add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("publisher/add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(publisher);
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

            return View(publisher);
        }
        
        [Route("publisher/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var publisherToUpdate = await _dbContext.Publisher.FirstOrDefaultAsync(c => c.Id == id);


            return View(publisherToUpdate);
        }
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Route("publisher/edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPublisher(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisherToUpdate = await _dbContext.Publisher.FirstOrDefaultAsync(p => p.Id == id);
            if (await TryUpdateModelAsync<Publisher>(
                 publisherToUpdate,
                "",
                p => p.Name
                )
            )
                
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                    
                    ViewData["ErrorMessage"] =
                        "Edit failed. Try again.";
                }
            }

            return View(publisherToUpdate);
        }

        [Route("publisher/delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _dbContext.Publisher
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again.";
            }

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("publisher/delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _dbContext.Publisher.FindAsync(id);
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _dbContext.Publisher.Remove(publisher);
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