using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Categories : Controller
    {
        [Route("categories")]
        public IActionResult Index()
        {
            return View();
        }
    }
}