using Microsoft.AspNetCore.Mvc;
using hvpLesson4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hvpLesson4.Controllers
{
    public class hvpProductController : Controller
    {
        private static readonly List<hvpCategory> Categories = new List<hvpCategory>
        {
            new hvpCategory { Id = 1, Name = "Quần Áo" },
            new hvpCategory { Id = 2, Name = "Túi xách" },
            new hvpCategory { Id = 3, Name = "Đồng hồ" },
            new hvpCategory { Id = 4, Name = "Tivi" },
            new hvpCategory { Id = 5, Name = "Tủ lạnh" },
            new hvpCategory { Id = 6, Name = "Máy bơm" },
            new hvpCategory { Id = 7, Name = "Quạt điện" },
            new hvpCategory { Id = 8, Name = "Lò sưởi" }
        };

        private static readonly List<hvpProduct> Products = new List<hvpProduct>
        {
            new hvpProduct
            {
                Id = 1,
                Name = "Bộ đồ bơi cho trẻ em nam",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 50000,
                Price = 35000,
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciats qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 7, 15, 0, 0, 0),
                CategoryId = 1
            },
            new hvpProduct
            {
                Id = 2,
                Name = "Bộ đồ bơi cho trẻ em nữ",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 50000,
                Price = 35000,
                Description = "Sản phẩm đồ bơi nữ cao cấp dành cho trẻ em chất liệu co giãn 4 chiều thoáng mát.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 7, 15, 0, 0, 0),
                CategoryId = 1
            },
            new hvpProduct
            {
                Id = 3,
                Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 50000,
                Price = 35000,
                Description = "Bộ đồ bơi dễ thương vừa vặn dành riêng cho bé từ 3 đến 5 tuổi.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 7, 15, 0, 0, 0),
                CategoryId = 1
            },
            new hvpProduct
            {
                Id = 4,
                Name = "Túi xách cao cấp ELLY",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 1200000,
                Price = 850000,
                Description = "Túi xách thời trang cao cấp thương hiệu ELLY chất liệu da bò sang trọng bền đẹp.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 8, 10, 0, 0, 0),
                CategoryId = 2
            },
            new hvpProduct
            {
                Id = 5,
                Name = "Đồng hồ nam dây da",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 2500000,
                Price = 1990000,
                Description = "Đồng hồ nam dây da chống nước phong cách hiện đại lịch lãm.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 9, 1, 0, 0, 0),
                CategoryId = 3
            },
            new hvpProduct
            {
                Id = 6,
                Name = "Tivi LG 4K 55 inch",
                Image = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                OldPrice = 15000000,
                Price = 12500000,
                Description = "Tivi thông minh LG 55 inch màn hình 4K HDR âm thanh sống động trung thực.",
                Status = "Còn hàng",
                CreatedDate = new DateTime(2021, 10, 5, 0, 0, 0),
                CategoryId = 4
            }
        };

        public IActionResult Index(int? categoryId)
        {
            ViewBag.Categories = Categories;
            var products = Products;
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
