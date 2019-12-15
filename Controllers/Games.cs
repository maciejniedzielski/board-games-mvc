using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Games : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}