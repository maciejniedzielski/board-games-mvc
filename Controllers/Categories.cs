using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        [Route("category/{id}")]
        public IActionResult Detail(int id)
        {
            var category = _dbContext.Category.First(c => c.Id == id);

            return View(category);
        }

        [Route("category/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("category/add")]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(category);
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

            return View(category);
        }
        
        [Route("category/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var categoryToUpdate = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);


            return View(categoryToUpdate);
        }
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Route("category/edit/{id}")]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryToUpdate = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (await TryUpdateModelAsync<Category>(
                categoryToUpdate,
                "",
                c => c.Name
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

            return View(categoryToUpdate);
        }

        [Route("category/delete/{id}")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dbContext.Category
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again.";
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("category/delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _dbContext.Category.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _dbContext.Category.Remove(category);
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