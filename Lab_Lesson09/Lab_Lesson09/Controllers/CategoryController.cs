using System.Linq;
using Lab_Lesson09.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_Lesson09.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public IActionResult Index()
        {
            var categories = DataStore.GetCategories();
            return View(categories);
        }
    }
}
