using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hvphuc_lesson2.Controllers
{
    [Route("loai-san-pham")]
    public class CategoryController : Controller
    {
        [Route("")]
        [Route("tat-ca")]
        public IActionResult Index()
        {
            string[] categories = { "Laptop", "Máy để bàn", "Điều hòa", "Máy giặt" };
            ViewBag.categories = categories;
            return View();
        }

        [Route("tim-kiem/{name}")]
        public IActionResult Search(string name)
        {
            string[] categories = { "Laptop", "Máy để bàn", "Điều hòa", "Máy giặt" };
            ViewBag.categories = categories.Where(x => x.Contains(name, System.StringComparison.OrdinalIgnoreCase)).ToArray();
            return View("Index");
        }
    }
}
