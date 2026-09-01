using Microsoft.AspNetCore.Mvc;
using hvpLesson4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hvpLesson4.Controllers
{
    public class AccountController : Controller
    {
        private static readonly List<hvpAccount> Accounts = new List<hvpAccount>
        {
            new hvpAccount
            {
                Id = 1,
                FullName = "Nguyễn Văn A",
                Email = "nguyenvana@gmail.com",
                Phone = "0912345678",
                Address = "Hà Nội",
                Avatar = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                Bio = "Developer & Tech enthusiast",
                Gender = "Male",
                Birthday = new DateTime(1995, 5, 20)
            },
            new hvpAccount
            {
                Id = 2,
                FullName = "Trần Thị B",
                Email = "tranthib@gmail.com",
                Phone = "0987654321",
                Address = "Đà Nẵng",
                Avatar = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                Bio = "UI/UX Designer",
                Gender = "Female",
                Birthday = new DateTime(1998, 8, 12)
            },
            new hvpAccount
            {
                Id = 3,
                FullName = "Hoàng Thúy",
                Email = "thuy@gmail.com",
                Phone = "0986456789",
                Address = "Hà Nội",
                Avatar = "/images/481335020_2107985623049481_2680113604465712083_n.jpg",
                Bio = "My name is small",
                Gender = "Male",
                Birthday = new DateTime(1999, 7, 15, 12, 0, 0)
            }
        };

        public IActionResult Index()
        {
            return View(Accounts);
        }

        [Route("ho-so-cua-toi")]
        [Route("Account/Profile/{id?}")]
        public IActionResult Profile(int id = 3)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                account = Accounts.FirstOrDefault(a => a.Id == 3) ?? Accounts.FirstOrDefault();
            }
            return View(account);
        }
    }
}
