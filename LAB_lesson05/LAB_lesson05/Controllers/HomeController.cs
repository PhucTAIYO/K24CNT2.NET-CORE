using LAB_lesson05.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAB_lesson05.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var latestProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                },
                new Product
                {
                    Id = 2,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                },
                new Product
                {
                    Id = 3,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                }
            };

            return View(latestProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
