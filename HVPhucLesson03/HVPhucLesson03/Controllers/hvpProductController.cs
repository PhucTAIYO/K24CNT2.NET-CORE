using Microsoft.AspNetCore.Mvc;
using HVPhucLesson03.Models;

namespace HVPhucLesson03.Controllers
{
    public class hvpProductController : Controller
    {
        private readonly List<HVPproduct> _products = new List<HVPproduct>
        {
            new HVPproduct
        {
            hvpproductID = "HVP001",
            hvpproductName = "Laptop Dell Inspiron",
            hvpyearReLease = 2024,
            hvpprice = 15000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP002",
            hvpproductName = "Laptop HP Pavilion",
            hvpyearReLease = 2023,
            hvpprice = 18000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP003",
            hvpproductName = "MacBook Air M2",
            hvpyearReLease = 2024,
            hvpprice = 25000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP004",
            hvpproductName = "Chuột Logitech",
            hvpyearReLease = 2022,
            hvpprice = 500000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP005",
            hvpproductName = "Bàn phím cơ Keychron",
            hvpyearReLease = 2023,
            hvpprice = 2000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP006",
            hvpproductName = "Màn hình Samsung 24 inch",
            hvpyearReLease = 2024,
            hvpprice = 3500000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP007",
            hvpproductName = "Tai nghe Sony",
            hvpyearReLease = 2023,
            hvpprice = 4500000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP008",
            hvpproductName = "Điện thoại Samsung Galaxy",
            hvpyearReLease = 2024,
            hvpprice = 12000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP009",
            hvpproductName = "Máy tính bảng iPad",
            hvpyearReLease = 2023,
            hvpprice = 16000000m
        },
        new HVPproduct
        {
            hvpproductID = "HVP010",
            hvpproductName = "Ổ cứng SSD Kingston",
            hvpyearReLease = 2024,
            hvpprice = 1800000m
        }
        };

        public IActionResult Index()
        {
            return Json(_products);
        }
        public IActionResult hvpGetAllProduct()
        {
            ViewData["Products"] = _products;
            return View(_products);
        }
    }
}
