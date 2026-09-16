using Microsoft.AspNetCore.Mvc;
using LABLesson08.Models;

namespace LABLesson08.Controllers
{
    public class PeopleController : Controller
    {
        // GET: PeopleController
        public ActionResult Index()
        {
            var peoples = DataLocal.GetPeoples();
            return View(peoples);
        }

        // GET: PeopleController/Details/5
        public ActionResult Details(int id)
        {
            var people = DataLocal.GetPeopleById(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // GET: PeopleController/Create
        public ActionResult Create()
        {
            var model = new People
            {
                Id = 0,
                Birthday = DateTime.Now.AddYears(-20),
                Gender = 1
            };
            return View(model);
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(People model, IFormFile? AvatarFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var peoples = DataLocal.GetPeoples();
                    model.Id = peoples.Any() ? peoples.Max(p => p.Id) + 1 : 1;

                    // Xử lý upload file avatar nếu có
                    if (AvatarFile != null && AvatarFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AvatarFile.FileName);
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        var filePath = Path.Combine(folderPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await AvatarFile.CopyToAsync(stream);
                        }
                        model.Avatar = "images/avatar/" + fileName;
                    }
                    else if (string.IsNullOrWhiteSpace(model.Avatar))
                    {
                        model.Avatar = "images/avatar/00.png";
                    }

                    peoples.Add(model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: PeopleController/Edit/5
        public ActionResult Edit(int id)
        {
            var people = DataLocal.GetPeopleById(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, People model, IFormFile? AvatarFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = DataLocal.GetPeopleById(id);
                    if (existing == null)
                    {
                        return NotFound();
                    }

                    existing.Name = model.Name;
                    existing.Email = model.Email;
                    existing.Phone = model.Phone;
                    existing.Address = model.Address;
                    existing.Birthday = model.Birthday;
                    existing.Bio = model.Bio;
                    existing.Gender = model.Gender;

                    // Xử lý upload file mới nếu người dùng chọn file
                    if (AvatarFile != null && AvatarFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(AvatarFile.FileName);
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatar");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        var filePath = Path.Combine(folderPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await AvatarFile.CopyToAsync(stream);
                        }
                        existing.Avatar = "images/avatar/" + fileName;
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: PeopleController/Delete/5
        public ActionResult Delete(int id)
        {
            var people = DataLocal.GetPeopleById(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: PeopleController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var people = DataLocal.GetPeopleById(id);
                if (people != null)
                {
                    DataLocal.GetPeoples().Remove(people);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
