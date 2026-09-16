using Microsoft.AspNetCore.Mvc;
using HVPLesson08.Models;

namespace HVPLesson08.Controllers
{
    public class HVPMemberController : Controller
    {
        // Mock data - HVPMember
        private static List<HVPMember> _members = new List<HVPMember>()
        {
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "PhucHvp",
                HVPPassword = "Password123!",
                HVPFullName = "Hoàng Văn Phúc",
                HVPEmail = "hoangvanphuc@gmail.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "tranthib",
                HVPPassword = "SecurePass456#",
                HVPFullName = "Trần Thị B",
                HVPEmail = "tranthib@outlook.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "levanc",
                HVPPassword = "MyPassword789$",
                HVPFullName = "Lê Văn C",
                HVPEmail = "levanc@company.com"
            }
        };

        // GET: Danh sách thành viên
        public IActionResult Index()
        {
            return View(_members);
        }

        [HttpGet]
        public IActionResult HVPCreate()
        {
            var member = new HVPMember();
            return View(member);
        }

        [HttpPost]
        public IActionResult HVPCreate(HVPMember hvpMember)
        {
            hvpMember.HVPMemberId = Guid.NewGuid().ToString();
            _members.Add(hvpMember);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult HVPEdit(string id)
        {
            var member = _members.Where(x => x.HVPMemberId.Equals(id)).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public IActionResult HVPEdit(string id, HVPMember hvpMember)
        {
            for (int i = 0; i < _members.Count; i++)
            {
                if (_members[i].HVPMemberId == id)
                {
                    _members[i].HVPUserName = hvpMember.HVPUserName;
                    _members[i].HVPPassword = hvpMember.HVPPassword;
                    _members[i].HVPFullName = hvpMember.HVPFullName;
                    _members[i].HVPEmail = hvpMember.HVPEmail;

                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult HVPDetails(string id)
        {
            var member = _members.Where(x => x.HVPMemberId.Equals(id)).FirstOrDefault();
            return View(member);
        }

        [HttpGet]
        public IActionResult HVPDelete(string id)
        {
            var member = _members.Where(x => x.HVPMemberId.Equals(id)).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public IActionResult HVPDeleted(string id)
        {
            foreach (var item in _members)
            {
                if (item.HVPMemberId.Equals(id))
                {
                    _members.Remove(item);
                    return RedirectToAction("Index");
                }
            }
            return View("HVPDelete");
        }
    }
}
