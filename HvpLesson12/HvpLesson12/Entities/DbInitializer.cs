using System;
using System.IO;
using System.Linq;
using HvpLesson12.Models;
using Microsoft.AspNetCore.Hosting;

namespace HvpLesson12.Entities
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext appContext, StudentDbContext studentContext, IWebHostEnvironment env)
        {
            // Ensure directories exist
            string webRoot = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string productDir = Path.Combine(webRoot, "Product");
            string bannerDir = Path.Combine(webRoot, "Banner");
            string studentDir = Path.Combine(webRoot, "Student");

            Directory.CreateDirectory(productDir);
            Directory.CreateDirectory(bannerDir);
            Directory.CreateDirectory(studentDir);

            // Ensure database schemas exist
            appContext.Database.EnsureCreated();
            studentContext.Database.EnsureCreated();

            // Seed AppDbContext (NetCoreCRUD)
            if (!appContext.Categories.Any())
            {
                var cat1 = new Category { Name = "Áo nam", Status = 1, CreatedDate = DateTime.Now };
                var cat2 = new Category { Name = "Áo nữ", Status = 1, CreatedDate = DateTime.Now };
                var cat3 = new Category { Name = "Quần jean", Status = 1, CreatedDate = DateTime.Now };
                var cat4 = new Category { Name = "Phụ kiện", Status = 1, CreatedDate = DateTime.Now };

                appContext.Categories.AddRange(cat1, cat2, cat3, cat4);
                appContext.SaveChanges();

                if (!appContext.Products.Any())
                {
                    appContext.Products.AddRange(
                        new Product
                        {
                            Name = "Demo product 1 0",
                            Price = 550000,
                            SalePrice = 450000,
                            Status = 1,
                            Descriptions = "Mô tả sản phẩm áo nam cao cấp thời trang phong cách trẻ trung",
                            CategoryId = cat1.Id,
                            CreatedDate = DateTime.Now,
                            Image = "product1.png"
                        },
                        new Product
                        {
                            Name = "Demo Product 2 0",
                            Price = 550000,
                            SalePrice = 450000,
                            Status = 1,
                            Descriptions = "Mô tả sản phẩm áo polo nam thể thao thoáng mát",
                            CategoryId = cat1.Id,
                            CreatedDate = DateTime.Now,
                            Image = "product2.png"
                        },
                        new Product
                        {
                            Name = "Demo product 3 0",
                            Price = 650000,
                            SalePrice = 490000,
                            Status = 1,
                            Descriptions = "Mô tả sản phẩm áo sơ mi nữ thanh lịch công sở",
                            CategoryId = cat2.Id,
                            CreatedDate = DateTime.Now,
                            Image = "product3.png"
                        },
                        new Product
                        {
                            Name = "Demo Product 4 0",
                            Price = 720000,
                            SalePrice = 580000,
                            Status = 1,
                            Descriptions = "Mô tả sản phẩm quần jean nam skinny co giãn",
                            CategoryId = cat3.Id,
                            CreatedDate = DateTime.Now,
                            Image = "product4.png"
                        }
                    );
                    appContext.SaveChanges();
                }
            }

            if (!appContext.Banners.Any())
            {
                appContext.Banners.AddRange(
                    new Banner
                    {
                        Name = "Microsoft Azure",
                        Image = "banner1.png",
                        Description = "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps.",
                        CreatedDate = DateTime.Now,
                        Status = 1
                    },
                    new Banner
                    {
                        Name = "Summer Fashion 2026",
                        Image = "banner2.png",
                        Description = "Khám phá bộ sưu tập thời trang mùa hè mới nhất với nhiều ưu đãi hấp dẫn.",
                        CreatedDate = DateTime.Now,
                        Status = 1
                    }
                );
                appContext.SaveChanges();
            }

            // Seed StudentDbContext (StudentManager)
            if (!studentContext.StdClasses.Any())
            {
                var c1 = new StdClass { ClassName = "K24CNT2" };
                var c2 = new StdClass { ClassName = "K24CNT1" };
                var c3 = new StdClass { ClassName = "K24CNT3" };

                studentContext.StdClasses.AddRange(c1, c2, c3);
                studentContext.SaveChanges();

                var sub1 = new Subjects { SubjectName = "Lập trình C#" };
                var sub2 = new Subjects { SubjectName = "Cơ sở dữ liệu SQL Server" };
                var sub3 = new Subjects { SubjectName = "ASP.NET Core MVC" };
                var sub4 = new Subjects { SubjectName = "Thiết kế Web" };

                studentContext.Subjects.AddRange(sub1, sub2, sub3, sub4);
                studentContext.SaveChanges();

                var st1 = new Student
                {
                    StudentName = "Hoàng Văn Phúc",
                    StudentEmail = "hvp@example.com",
                    StudentPhone = "0987654321",
                    StudentAddress = "Hà Nội",
                    StudentAvatar = "avatar1.png",
                    StudentBirthday = new DateTime(2004, 5, 15),
                    ClassId = c1.Id
                };
                var st2 = new Student
                {
                    StudentName = "Trần Thị Lan",
                    StudentEmail = "lan.tt@example.com",
                    StudentPhone = "0912345678",
                    StudentAddress = "Hải Phòng",
                    StudentAvatar = "avatar2.png",
                    StudentBirthday = new DateTime(2004, 8, 20),
                    ClassId = c1.Id
                };
                var st3 = new Student
                {
                    StudentName = "Nguyễn Minh Đức",
                    StudentEmail = "duc.nm@example.com",
                    StudentPhone = "0934567890",
                    StudentAddress = "Nam Định",
                    StudentAvatar = "avatar3.png",
                    StudentBirthday = new DateTime(2003, 11, 10),
                    ClassId = c2.Id
                };

                studentContext.Students.AddRange(st1, st2, st3);
                studentContext.SaveChanges();

                studentContext.Marks.AddRange(
                    new Marks { StudentId = st1.Id, SubjectId = sub1.Id, Score = 9.0f },
                    new Marks { StudentId = st1.Id, SubjectId = sub2.Id, Score = 8.5f },
                    new Marks { StudentId = st1.Id, SubjectId = sub3.Id, Score = 9.5f },
                    new Marks { StudentId = st2.Id, SubjectId = sub1.Id, Score = 8.0f },
                    new Marks { StudentId = st2.Id, SubjectId = sub3.Id, Score = 8.5f },
                    new Marks { StudentId = st3.Id, SubjectId = sub2.Id, Score = 7.5f }
                );
                studentContext.SaveChanges();
            }

            // Create SVG placeholder images if not exist
            CreateSvgImageIfNotExists(Path.Combine(productDir, "product1.png"), "#3b82f6", "Sản phẩm 1");
            CreateSvgImageIfNotExists(Path.Combine(productDir, "product2.png"), "#10b981", "Sản phẩm 2");
            CreateSvgImageIfNotExists(Path.Combine(productDir, "product3.png"), "#f59e0b", "Sản phẩm 3");
            CreateSvgImageIfNotExists(Path.Combine(productDir, "product4.png"), "#ec4899", "Sản phẩm 4");

            CreateSvgImageIfNotExists(Path.Combine(bannerDir, "banner1.png"), "#1e40af", "Microsoft Azure Cloud");
            CreateSvgImageIfNotExists(Path.Combine(bannerDir, "banner2.png"), "#047857", "Bộ sưu tập 2026");

            CreateSvgImageIfNotExists(Path.Combine(studentDir, "avatar1.png"), "#6366f1", "HVP");
            CreateSvgImageIfNotExists(Path.Combine(studentDir, "avatar2.png"), "#ec4899", "TTL");
            CreateSvgImageIfNotExists(Path.Combine(studentDir, "avatar3.png"), "#14b8a6", "NMD");
        }

        private static void CreateSvgImageIfNotExists(string filePath, string color, string text)
        {
            if (File.Exists(filePath)) return;

            // Generate an SVG and save as fallback (SVG content readable by modern browsers even as .png or .svg)
            string svg = $@"<svg xmlns='http://www.w3.org/2000/svg' width='400' height='300' viewBox='0 0 400 300'>
  <rect width='400' height='300' fill='{color}' rx='12'/>
  <text x='50%' y='50%' dominant-baseline='middle' text-anchor='middle' font-family='sans-serif' font-size='22' font-weight='bold' fill='#ffffff'>{text}</text>
</svg>";
            File.WriteAllText(filePath, svg);
        }
    }
}
