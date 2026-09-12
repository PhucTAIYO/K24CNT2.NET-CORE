using Microsoft.AspNetCore.Mvc;
using HVPleeson07.Models.DataModels;

namespace HVPleeson07.Controllers
{
    public class HVPMemberController : Controller
    {
        // Mock Data
        protected static List<HVPMember> _members = new List<HVPMember>
        {
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "hvpphuc",
                HVPPassword = "123456",
                HVPFullName = "Hà Văn Phúc",
                HVPEmail = "phuchv@example.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "tranthibinh",
                HVPPassword = "123456",
                HVPFullName = "Trần Thị Bình",
                HVPEmail = "tranthibinh@example.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "levancuong",
                HVPPassword = "123456",
                HVPFullName = "Lê Văn Cường",
                HVPEmail = "levancuong@example.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "phamthiduyen",
                HVPPassword = "123456",
                HVPFullName = "Phạm Thị Duyên",
                HVPEmail = "phamthiduyen@example.com"
            },
            new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "hoangminhduc",
                HVPPassword = "123456",
                HVPFullName = "Hoàng Minh Đức",
                HVPEmail = "hoangminhduc@example.com"
            }
        };

        public IActionResult Index()
        {
            return View(_members);
        }

        public IActionResult GetMember()
        {
            var member = new HVPMember
            {
                HVPMemberId = Guid.NewGuid().ToString(),
                HVPUserName = "hvpphuc",
                HVPPassword = "password123",
                HVPFullName = "Hà Văn Phúc",
                HVPEmail = "phuchv@gmail.com"
            };
            //ViewBag.Member = member;
            return View(member);
        }

        // Đưa dữ liệu dạng List ra View
        public IActionResult GetMembers()
        {
            // Lấy từ mock data
            ViewBag.Members = _members;
            return View();
        }

        // GET: Create member
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create member
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HVPMember member)
        {
            if (ModelState.IsValid)
            {
                member.HVPMemberId = Guid.NewGuid().ToString();
                _members.Add(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
    }
}
