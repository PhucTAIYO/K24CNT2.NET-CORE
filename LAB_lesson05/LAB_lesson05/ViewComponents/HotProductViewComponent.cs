using Microsoft.AspNetCore.Mvc;
using LAB_lesson05.Models;

namespace LAB_lesson05.ViewComponents
{
    public class HotProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var hotProducts = new List<Product>
            {
                new Product
                {
                    Id = 101,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                },
                new Product
                {
                    Id = 102,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                },
                new Product
                {
                    Id = 103,
                    Name = "Nồi cơm điện cao tần Nagakawa NAG0102",
                    Image = "/images/nagakawa.jpg",
                    Price = 2850000
                }
            };

            return View(hotProducts);
        }
    }
}
