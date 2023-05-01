using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            if (this.User.IsInRole("Admin") && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                return RedirectToAction("login", "Account");
            }
            return View();
        }
    }
}
