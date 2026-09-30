using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvpLesson12.Entities;
using HvpLesson12.Models;

namespace HvpLesson12.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners
                .Where(b => b.Status == 1)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Status == 1)
                .OrderByDescending(p => p.CreatedDate)
                .Take(8)
                .ToListAsync();

            ViewBag.Banners = banners;
            return View(products);
        }

        // Bài 2: Action Product hiển thị danh sách sản phẩm dạng cột/card
        public async Task<IActionResult> Product(int? categoryId)
        {
            var categories = await _context.Categories.Where(c => c.Status == 1).ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = categoryId;

            var query = _context.Products.Include(p => p.Category).Where(p => p.Status == 1);
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = await query.OrderByDescending(p => p.CreatedDate).ToListAsync();
            return View(products);
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
