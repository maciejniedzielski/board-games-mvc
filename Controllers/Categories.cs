using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Categories : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}