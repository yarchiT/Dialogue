using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dialogue.Models;
using System.Web;
using Dialogue.Services;
using Microsoft.AspNetCore.Http;

namespace Dialogue.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(HttpContext.Session.GetString("useName") != null){
                 return View("~/Views/Chat.cshtml");
            }else{
                return View("~/Views/Login.cshtml");
            }
        }

        [HttpGet]
		public ActionResult Login()
		{
            return Index();
        }

        [HttpPost]
		public ActionResult Login(string username, string password)
		{
            ViewBag.CurrentUserName = username;
            HttpContext.Session.SetString("useName", username);
		    var res = ServiceConnector.Login(username, password);
            return Index();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            ViewBag.CurrentUserName = username;
            HttpContext.Session.SetString("useName", username);
            var res = ServiceConnector.Register(username, password);
            return Index();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
