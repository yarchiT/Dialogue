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
        [HttpGet]
        public IActionResult Index()
        {
           var username = HttpContext.Session.GetString("LoggedUserName");
           return View("Index", username);
        }

        [HttpGet]
		public IActionResult Login()
		{
            UserLoginViewModel user = new UserLoginViewModel();
            return View("Login", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Login( UserLoginViewModel user)
		{
            if (ModelState.IsValid)
            {
                var res = await ServiceConnector.Login(user.UserName, user.PasswordString);
                if (res is OkResult)
                {
                    HttpContext.Session.SetString("LoggedUserName", user.UserName);
                    return Index();
                }
                return View(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(user);
            }
		    
            return View(user);
        }

        [HttpGet]
		public ActionResult Register()
		{
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( UserLoginViewModel user)
        { 
            var res = await ServiceConnector.Register(user.UserName, user.PasswordString);
            if (res is OkResult)
            {
                HttpContext.Session.SetString("LoggedUserName", user.UserName);
                return Index();
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
