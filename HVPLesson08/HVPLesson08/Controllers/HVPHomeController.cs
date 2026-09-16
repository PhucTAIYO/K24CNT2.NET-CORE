using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HVPLesson08.Models;

namespace HVPLesson08.Controllers
{
    public class HVPHomeController : Controller
    {
        private readonly ILogger<HVPHomeController> _logger;

        public HVPHomeController(ILogger<HVPHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HVPIndex()
        {
            return View();
        }

        public IActionResult HVPPrivacy()
        {
            return View();
        }

        public IActionResult HVPAbout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
