using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Publishers : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}