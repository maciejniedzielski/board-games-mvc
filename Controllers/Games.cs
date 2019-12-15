using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Games : Controller
    {
        // GET
        [Route("games")]
        public IActionResult Index()
        {
            return View();
        }
    }
}