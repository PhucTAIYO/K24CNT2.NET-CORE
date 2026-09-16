using Lab_Lesson06.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_Lesson06.ViewComponents
{
    public class HotProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var hotProducts = new List<Product>
            {
                new Product
                {
                    Id = 4,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000
                },
                new Product
                {
                    Id = 5,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000
                },
                new Product
                {
                    Id = 6,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/noi-com-dien.jpg",
                    Price = 2450000
                }
            };

            return View(hotProducts);
        }
    }
}
