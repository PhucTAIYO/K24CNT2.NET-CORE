using Microsoft.AspNetCore.Mvc;

namespace Hvp_Lesson13.Controllers
{
    public class HvpProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string keyword)
        {
            ViewData["keyword"] = keyword;
            return View();
        }

        public IActionResult Hots()
        {
            return View();
        }
    }
}
