using Microsoft.AspNetCore.Mvc;
using HVP_Lesson09.Models.DataModels;
using HVP_Lesson09.Models.DataViewModels;

namespace HVP_Lesson09.Controllers
{
    public class HvpMemberController : Controller
    {
        private static readonly List<HvpMember> _hvpMembers = new List<HvpMember>
        {
            new HvpMember
            {
                HvpMemberId = 1,
                HvpUserName = "hvphuc",
                HvpPassword = "Password123@",
                HvpFullName = "Hoàng Văn Phúc",
                HvpEmail = "hvphuc@gmail.com",
                HvpPhoneNumber = "0912345678",
                HvpBirthday = "20/10/2004"
            },
            new HvpMember
            {
                HvpMemberId = 2,
                HvpUserName = "admin",
                HvpPassword = "AdminPassword123@",
                HvpFullName = "Quản trị viên",
                HvpEmail = "admin@gmail.com",
                HvpPhoneNumber = "0987654321",
                HvpBirthday = "01/01/2000"
            }
        };

        // GET: HvpMemberController
        public ActionResult Index()
        {
            return View(_hvpMembers);
        }

        // GET: HvpMemberController/Details/5
        public ActionResult Details(int id)
        {
            var member = _hvpMembers.FirstOrDefault(m => m.HvpMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: HvpMemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HvpMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HvpMemberRegister hvpMember)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(hvpMember);
                }

                int newId = hvpMember.HvpMemberId > 0 
                    ? hvpMember.HvpMemberId 
                    : (_hvpMembers.Count > 0 ? _hvpMembers.Max(m => m.HvpMemberId) + 1 : 1);

                _hvpMembers.Add(new HvpMember
                {
                    HvpMemberId = newId,
                    HvpUserName = hvpMember.HvpUserName,
                    HvpPassword = hvpMember.HvpPassword,
                    HvpEmail = hvpMember.HvpEmail,
                    HvpPhoneNumber = hvpMember.HvpPhoneNumber,
                    HvpFullName = hvpMember.HvpFullName,
                    HvpBirthday = hvpMember.HvpBirthday?.ToString("dd/MM/yyyy")
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(hvpMember);
            }
        }

        // GET: HvpMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _hvpMembers.FirstOrDefault(m => m.HvpMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: HvpMemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HvpMember member)
        {
            try
            {
                var existing = _hvpMembers.FirstOrDefault(m => m.HvpMemberId == id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.HvpUserName = member.HvpUserName;
                existing.HvpPassword = member.HvpPassword;
                existing.HvpFullName = member.HvpFullName;
                existing.HvpEmail = member.HvpEmail;
                existing.HvpPhoneNumber = member.HvpPhoneNumber;
                existing.HvpBirthday = member.HvpBirthday;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(member);
            }
        }

        // GET: HvpMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            var member = _hvpMembers.FirstOrDefault(m => m.HvpMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: HvpMemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var member = _hvpMembers.FirstOrDefault(m => m.HvpMemberId == id);
                if (member != null)
                {
                    _hvpMembers.Remove(member);
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
