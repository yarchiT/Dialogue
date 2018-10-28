using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dialogue.Models;
using System.Web;

namespace Dialogue.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Login.cshtml");
        }

        [HttpPost]
		public ActionResult Login(string username)
		{
            ViewBag.CurrentUserName = username;
            return View("~/Views/Chat.cshtml");
        }
        
        [HttpPost]
		public JsonResult HandleMessage(string message)
		{
            
            return Json(new {result = "Recieved " + message});
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
