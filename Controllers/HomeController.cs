using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dialogue.Models;
using System.Web;
using Dialogue.Services;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Dialogue.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
           var username = HttpContext.Session.GetString("LoggedUserName");
            List<Message> chat = await ServiceConnector.GetChatHistory(username);

            return View("Index", new ChatPageViewModel() { UserName = username, ChatHistory = chat });
        }

        [HttpGet]
		public IActionResult Login()
		{
            UserLoginViewModel user = new UserLoginViewModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLoginViewModel user)
		{
            if (ModelState.IsValid)
            {
                var res = await ServiceConnector.Login(user.UserName, user.PasswordString);
                if (res is OkResult)
                {
                    HttpContext.Session.SetString("LoggedUserName", user.UserName);

                    return RedirectToAction("Index");
                }else if(res is NotFoundResult)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect username or password");
                }
                return View(user);
            }

		    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(user);
        }

        [HttpGet]
		public ActionResult Register()
		{
            UserRegisterViewModel user = new UserRegisterViewModel();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( UserRegisterViewModel user)
        { 
            if (ModelState.IsValid)
            {
                var res = await ServiceConnector.Register(user.UserName, user.PasswordString);
                if (res is OkResult)
                {
                    HttpContext.Session.SetString("LoggedUserName", user.UserName);
                    return RedirectToAction("Index");
                }else
                {
                    ModelState.AddModelError(string.Empty, "User with with these credentials already exist.");
                    return View(user);
                }
            }
            
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
           
            return View(user);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
