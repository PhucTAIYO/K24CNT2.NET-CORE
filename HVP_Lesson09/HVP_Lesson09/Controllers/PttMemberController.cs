using Microsoft.AspNetCore.Mvc;
using HVP_Lesson09.Models.DataModels;
using HVP_Lesson09.Models.DataViewModels;

namespace HVP_Lesson09.Controllers
{
    public class PttMemberController : Controller
    {
        private static readonly List<PttMember> _pttMembers = new List<PttMember>
        {
            new PttMember
            {
                PttMemberId = 1,
                PttUserName = "hvphuc",
                PttPassword = "Password123@",
                PttFullName = "Hoàng Văn Phúc",
                PttEmail = "hvphuc@gmail.com",
                PttPhoneNumber = "0912345678",
                PttBirthday = "20/10/2004"
            },
            new PttMember
            {
                PttMemberId = 2,
                PttUserName = "admin",
                PttPassword = "AdminPassword123@",
                PttFullName = "Quản trị viên",
                PttEmail = "admin@gmail.com",
                PttPhoneNumber = "0363106800",
                PttBirthday = "01/01/2000"
            }
        };

        // GET: PttMemberController
        public ActionResult Index()
        {
            return View(_pttMembers);
        }

        // GET: PttMemberController/Details/5
        public ActionResult Details(int id)
        {
            var member = _pttMembers.FirstOrDefault(m => m.PttMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: PttMemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PttMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PttMemberRegister pttMember)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pttMember);
                }

                int newId = pttMember.PttMemberId > 0
                    ? pttMember.PttMemberId
                    : (_pttMembers.Count > 0 ? _pttMembers.Max(m => m.PttMemberId) + 1 : 1);

                _pttMembers.Add(new PttMember
                {
                    PttMemberId = newId,
                    PttUserName = pttMember.PttUserName,
                    PttPassword = pttMember.PttPassword,
                    PttEmail = pttMember.PttEmail,
                    PttPhoneNumber = pttMember.PttPhoneNumber,
                    PttFullName = pttMember.PttFullName,
                    PttBirthday = pttMember.PttBirthday?.ToString("dd/MM/yyyy")
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(pttMember);
            }
        }

        // GET: PttMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _pttMembers.FirstOrDefault(m => m.PttMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: PttMemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PttMember member)
        {
            try
            {
                var existing = _pttMembers.FirstOrDefault(m => m.PttMemberId == id);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.PttUserName = member.PttUserName;
                existing.PttPassword = member.PttPassword;
                existing.PttFullName = member.PttFullName;
                existing.PttEmail = member.PttEmail;
                existing.PttPhoneNumber = member.PttPhoneNumber;
                existing.PttBirthday = member.PttBirthday;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(member);
            }
        }

        // GET: PttMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            var member = _pttMembers.FirstOrDefault(m => m.PttMemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: PttMemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var member = _pttMembers.FirstOrDefault(m => m.PttMemberId == id);
                if (member != null)
                {
                    _pttMembers.Remove(member);
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
