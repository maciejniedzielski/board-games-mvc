using Microsoft.AspNetCore.Mvc;

namespace BoardGames.Controllers
{
    public class Publishers : Controller
    {
        [Route("publishers")]
        public IActionResult Index()
        {
            return View();
        }
    }
}