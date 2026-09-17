using Microsoft.AspNetCore.Mvc;
using LABlesson04.Models;

namespace LABlesson04.Controllers
{
    [Route("san-pham")]
    [Route("Product")]
    public class ProductController : Controller
    {
        // Dữ liệu mẫu danh mục
        private static readonly List<Category> _categories = new()
        {
            new Category { Id = 1, Name = "Quần Áo" },
            new Category { Id = 2, Name = "Túi xách" },
            new Category { Id = 3, Name = "Đồng hồ" },
            new Category { Id = 4, Name = "Ti vi" },
            new Category { Id = 5, Name = "Tủ lạnh" },
            new Category { Id = 6, Name = "Máy bơm" },
            new Category { Id = 7, Name = "Quạt điện" },
            new Category { Id = 8, Name = "Lò sưởi" }
        };

        // Dữ liệu mẫu sản phẩm
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                Id = 1,
                Name = "Bộ đồ bơi cho trẻ em nam",
                Image = "/images/boi-nam.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 1,
                Description = "Bộ đồ bơi cho trẻ em nam chất liệu cao cấp, thoáng mát, co giãn 4 chiều và chống tia cực tím UV giúp bé thỏa sức bơi lội.",
                Status = true,
                CreatedAt = new DateTime(2024, 5, 10, 8, 30, 0)
            },
            new Product
            {
                Id = 2,
                Name = "Bộ đồ bơi cho trẻ em nữ",
                Image = "/images/boi-nu.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 1,
                Description = "Bộ đồ bơi cho trẻ em nữ thiết kế dễ thương, màu sắc tươi sáng, chất vải mau khô bảo vệ làn da nhạy cảm của bé.",
                Status = true,
                CreatedAt = new DateTime(2024, 5, 12, 9, 15, 0)
            },
            new Product
            {
                Id = 3,
                Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi",
                Image = "/images/boi-3-5.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 1,
                Description = "Bộ đồ bơi thiết kế chuyên biệt cho các bé từ 3 đến 5 tuổi, đường may tỉ mỉ, khóa kéo êm ái chống kẹt da.",
                Status = true,
                CreatedAt = new DateTime(2024, 5, 15, 14, 0, 0)
            },
            new Product
            {
                Id = 4,
                Name = "Bộ đồ bơi cho trẻ em thời trang",
                Image = "/images/boi-thoi-trang.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 1,
                Description = "Bộ đồ bơi phong cách thời trang hiện đại với họa tiết cá tính, chất thun lạnh co giãn tối ưu khi hoạt động thể thao nước.",
                Status = true,
                CreatedAt = new DateTime(2024, 5, 18, 16, 45, 0)
            },
            new Product
            {
                Id = 5,
                Name = "Túi thời trang mẫu mới 2021",
                Image = "/images/tui-2021.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 2,
                Description = "Túi thời trang nữ phong cách Hàn Quốc mẫu mới, đường kim mũi chỉ tinh tế cùng quai xách chắc chắn, tiện lợi đi làm hay đi chơi.",
                Status = true,
                CreatedAt = new DateTime(2024, 6, 1, 10, 0, 0)
            },
            new Product
            {
                Id = 6,
                Name = "Túi thời trang da cá sấu",
                Image = "/images/tui-ca-sau.svg",
                Price = 50000,
                SalePrice = 35000,
                CategoryId = 2,
                Description = "Túi thời trang da cá sấu cao cấp, khóa kim loại mạ vàng sang trọng, tạo phong cách quý phái và đẳng cấp cho phái đẹp.",
                Status = true,
                CreatedAt = new DateTime(2024, 6, 5, 11, 20, 0)
            },
            new Product
            {
                Id = 7,
                Name = "Đồng hồ cao cấp thời trang",
                Image = "/images/dong-ho.svg",
                Price = 120000,
                SalePrice = 99000,
                CategoryId = 3,
                Description = "Đồng hồ đeo tay thiết kế tinh xảo, mặt kính khoáng chống trầy, khả năng chịu nước 30m thích hợp cho mọi hoạt động hàng ngày.",
                Status = true,
                CreatedAt = new DateTime(2024, 6, 10, 15, 0, 0)
            }
        };

        // GET: /san-pham hoặc /san-pham?categoryId=1 hoặc /san-pham/danh-muc/1
        [HttpGet("")]
        [HttpGet("Index")]
        [HttpGet("danh-muc/{categoryId}")]
        public IActionResult Index(int? categoryId)
        {
            var productList = _products;

            // Nếu người dùng chọn lọc theo danh mục
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                productList = _products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            var viewModel = new ProductViewModel
            {
                Products = productList,
                Categories = _categories,
                SelectedCategoryId = categoryId
            };

            // Đồng thời gán vào ViewBag để linh hoạt cho các cách dùng trong bài thực hành
            ViewBag.Categories = _categories;
            ViewBag.Products = productList;
            ViewBag.SelectedCategoryId = categoryId;

            return View(viewModel);
        }

        // GET: /san-pham/detail/1 hoặc /san-pham/chi-tiet/1
        [HttpGet("detail/{id}")]
        [HttpGet("chi-tiet/{id}")]
        [HttpGet("Details/{id}")]
        public IActionResult Detail(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
            ViewBag.CategoryName = category != null ? category.Name : "Khác";

            return View(product);
        }
    }
}
