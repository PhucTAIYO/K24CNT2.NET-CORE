using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_Lesson09.Models
{
    public static class DataStore
    {
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<Product> Products { get; set; } = new List<Product>();

        static DataStore()
        {
            InitializeData();
        }

        public static void InitializeData()
        {
            Categories = new List<Category>
            {
                new Category { Id = 1, Name = "Điện thoại thông minh" },
                new Category { Id = 2, Name = "Laptop & Máy tính xách tay" },
                new Category { Id = 3, Name = "Máy tính bảng" },
                new Category { Id = 4, Name = "Đồng hồ thông minh" },
                new Category { Id = 5, Name = "Phụ kiện công nghệ" }
            };

            Products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max 256GB Titan Tự Nhiên",
                    Image = "iphone-15-pro-max.png",
                    Price = 30990000f,
                    SalePrice = 27490000f,
                    Description = "Siêu phẩm Apple với khung viền Titan cao cấp, chip A17 Pro mạnh mẽ và cụm camera telephoto 5x chuyên nghiệp.",
                    CategoryId = 1,
                    Category = Categories.FirstOrDefault(c => c.Id == 1)
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S24 Ultra 5G 12GB/256GB",
                    Image = "galaxy-s24-ultra.png",
                    Price = 29990000f,
                    SalePrice = 25990000f,
                    Description = "Trải nghiệm quyền năng Galaxy AI đỉnh cao với bút S-Pen tích hợp và camera cảm biến 200MP xuất sắc.",
                    CategoryId = 1,
                    Category = Categories.FirstOrDefault(c => c.Id == 1)
                },
                new Product
                {
                    Id = 3,
                    Name = "MacBook Pro 14 inch M3 Pro 18GB/512GB",
                    Image = "macbook-pro-m3.png",
                    Price = 49990000f,
                    SalePrice = 43990000f,
                    Description = "Laptop chuyên nghiệp cho đồ họa, lập trình và sáng tạo nội dung với thời lượng pin lên đến 22 tiếng liên tục.",
                    CategoryId = 2,
                    Category = Categories.FirstOrDefault(c => c.Id == 2)
                },
                new Product
                {
                    Id = 4,
                    Name = "iPad Air 6 M2 11 inch 128GB Wifi",
                    Image = "ipad-air-m2.png",
                    Price = 16990000f,
                    SalePrice = 14990000f,
                    Description = "Hiệu năng đột phá từ chip Apple M2, hỗ trợ Apple Pencil Pro mới và bàn phím Magic Keyboard tiện lợi.",
                    CategoryId = 3,
                    Category = Categories.FirstOrDefault(c => c.Id == 3)
                },
                new Product
                {
                    Id = 5,
                    Name = "Tai nghe chống ồn Sony WH-1000XM5",
                    Image = "sony-wh-1000xm5.png",
                    Price = 8490000f,
                    SalePrice = 6990000f,
                    Description = "Công nghệ chống ồn hàng đầu thế giới với vi xử lý kép V1 và QN1, âm thanh Hi-Res Audio không dây sắc nét.",
                    CategoryId = 5,
                    Category = Categories.FirstOrDefault(c => c.Id == 5)
                }
            };
        }

        // Methods for Categories
        public static List<Category> GetCategories() => Categories;

        public static Category? GetCategoryById(int id) => Categories.FirstOrDefault(c => c.Id == id);

        public static bool CategoryExists(int id) => Categories.Any(c => c.Id == id);

        // Methods for Products
        public static List<Product> GetProducts()
        {
            // Gán Category navigation property nếu bị null
            foreach (var p in Products)
            {
                if (p.Category == null || p.Category.Id != p.CategoryId)
                {
                    p.Category = Categories.FirstOrDefault(c => c.Id == p.CategoryId);
                }
            }
            return Products;
        }

        public static Product? GetProductById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            }
            return product;
        }

        public static void AddProduct(Product product)
        {
            int nextId = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
            product.Id = nextId;
            product.Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            Products.Insert(0, product);
        }

        public static bool UpdateProduct(Product product)
        {
            var existing = Products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null) return false;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.SalePrice = product.SalePrice;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;
            existing.Category = Categories.FirstOrDefault(c => c.Id == product.CategoryId);

            if (!string.IsNullOrEmpty(product.Image))
            {
                existing.Image = product.Image;
            }

            return true;
        }

        public static bool DeleteProduct(int id)
        {
            var existing = Products.FirstOrDefault(p => p.Id == id);
            if (existing != null)
            {
                return Products.Remove(existing);
            }
            return false;
        }
    }
}
