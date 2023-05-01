using BBMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string returnUrl)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "DashBoard", new { area="admin"});
                }
                else
                {
                    return RedirectToAction("Index", "DashBoard");
                }
                
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Privacy()
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
