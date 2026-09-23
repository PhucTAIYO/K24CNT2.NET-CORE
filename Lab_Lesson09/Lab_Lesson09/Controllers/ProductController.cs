using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab_Lesson09.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_Lesson09.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public IActionResult Index(string? searchString, int? categoryId)
        {
            var products = DataStore.GetProducts().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                               p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = DataStore.GetCategories();
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategoryId = categoryId;

            return View(products.ToList());
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var product = DataStore.GetProductById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm có mã: " + id;
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            PopulateCategoriesDropDownList();
            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // Kiểm tra CategoryId có tồn tại trong danh sách Category không
            if (!DataStore.CategoryExists(product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Danh mục được chọn không hợp lệ hoặc không tồn tại.");
            }

            // Kiểm tra Image bắt buộc phải upload
            if (product.ImageFile == null || product.ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Thuộc tính hình ảnh bắt buộc phải chọn và upload file.");
            }
            else
            {
                var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var ext = Path.GetExtension(product.ImageFile.FileName).ToLowerInvariant();
                if (!allowedExts.Contains(ext))
                {
                    ModelState.AddModelError("ImageFile", "File hình ảnh không hợp lệ. Chỉ chấp nhận các định dạng .jpg, .jpeg, .png, .gif, .webp.");
                }
            }

            if (ModelState.IsValid)
            {
                // Upload ảnh vào thư mục wwwroot/products
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string ext = Path.GetExtension(product.ImageFile!.FileName).ToLowerInvariant();
                string uniqueFileName = $"{Guid.NewGuid():N}_{DateTime.Now.Ticks}{ext}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.Image = uniqueFileName;
                DataStore.AddProduct(product);

                TempData["SuccessMessage"] = $"Thêm mới sản phẩm '{product.Name}' thành công!";
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = DataStore.GetProductById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm có mã: " + id;
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                TempData["ErrorMessage"] = "Mã sản phẩm không khớp.";
                return RedirectToAction(nameof(Index));
            }

            var existingProduct = DataStore.GetProductById(id);
            if (existingProduct == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm cần cập nhật.";
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra CategoryId
            if (!DataStore.CategoryExists(product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Danh mục được chọn không hợp lệ hoặc không tồn tại.");
            }

            // Kiểm tra nếu có upload ảnh mới
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var ext = Path.GetExtension(product.ImageFile.FileName).ToLowerInvariant();
                if (!allowedExts.Contains(ext))
                {
                    ModelState.AddModelError("ImageFile", "File hình ảnh không hợp lệ. Chỉ chấp nhận các định dạng .jpg, .jpeg, .png, .gif, .webp.");
                }
            }

            // Bỏ qua lỗi validation cho Image và ImageFile nếu đang giữ lại ảnh cũ
            if (product.ImageFile == null && !string.IsNullOrEmpty(existingProduct.Image))
            {
                ModelState.Remove("ImageFile");
                product.Image = existingProduct.Image;
            }

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string ext = Path.GetExtension(product.ImageFile.FileName).ToLowerInvariant();
                    string uniqueFileName = $"{Guid.NewGuid():N}_{DateTime.Now.Ticks}{ext}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    product.Image = uniqueFileName;
                }
                else
                {
                    product.Image = existingProduct.Image;
                }

                DataStore.UpdateProduct(product);
                TempData["SuccessMessage"] = $"Cập nhật sản phẩm '{product.Name}' thành công!";
                return RedirectToAction(nameof(Index));
            }

            // Nếu không chọn file mới thì vẫn giữ lại tên ảnh cũ để hiển thị preview
            if (string.IsNullOrEmpty(product.Image))
            {
                product.Image = existingProduct.Image;
            }

            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = DataStore.GetProductById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm có mã: " + id;
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = DataStore.GetProductById(id);
            if (product != null)
            {
                DataStore.DeleteProduct(id);
                TempData["SuccessMessage"] = $"Đã xóa sản phẩm '{product.Name}' thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm để xóa.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateCategoriesDropDownList(object? selectedCategory = null)
        {
            var categories = DataStore.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", selectedCategory);
        }
    }
}
