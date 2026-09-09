using hvpLesson6.Models;
using Microsoft.AspNetCore.Mvc;

namespace hvpLesson6.ViewcComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int n = 0)
        {
            List<Category> categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Đồ điện tử", isActive = true },
                new Category { CategoryID = 2, CategoryName = "Thời trang", isActive = true },
                new Category { CategoryID = 3, CategoryName = "Sách", isActive = false },
                new Category { CategoryID = 4, CategoryName = "Gia dụng & Đời sống", isActive = true },
                new Category { CategoryID = 5, CategoryName = "Thể thao & Dã ngoại", isActive = false }
            };

            // Lọc theo tham số n (CategoryID > n)
            var search = categories.Where(s => s.CategoryID > n);
            return View(search);
        }
    }
}

