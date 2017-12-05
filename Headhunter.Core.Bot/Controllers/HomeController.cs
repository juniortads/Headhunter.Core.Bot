using Microsoft.AspNetCore.Mvc;

namespace Headhunter.Core.Bot.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
