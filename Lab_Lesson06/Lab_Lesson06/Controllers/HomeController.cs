using Lab_Lesson06.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab_Lesson06.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Yêu cầu: Phần sản phẩm mới nhất load từ Action Index của HomeController
        public IActionResult Index()
        {
            var newestProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000,
                    Description = "Dung tích 1.8L, công nghệ cao tần IH"
                },
                new Product
                {
                    Id = 2,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000,
                    Description = "Dung tích 1.8L, công nghệ cao tần IH"
                },
                new Product
                {
                    Id = 3,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000,
                    Description = "Dung tích 1.8L, công nghệ cao tần IH"
                }
            };

            return View(newestProducts);
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
