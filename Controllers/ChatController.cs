using System.Threading.Tasks;
using Dialogue.Models;
using Dialogue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dialogue.Controllers
{
    public class ChatController : Controller
    {
        IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
		public async Task<JsonResult> HandleMessage(string message)
		{
		    var username = HttpContext.Session.GetString("LoggedUserName");
		    (IActionResult res, bool serviceIsRunning) = await ServiceConnector.AddMessage(username, new MessageDto(){Author = AuthorId.User, Text = message});
		    if (serviceIsRunning)
		    {
		        if (res is OkResult)
		        {
		            string response = _chatService.responseToMessage(message);
		            (IActionResult responseFromSiri, bool serviceIsOk) = await ServiceConnector.AddMessage(username,
		                new MessageDto() {Author = AuthorId.Siri, Text = response});
		            
					if (responseFromSiri is OkResult)
		            {
		                return Json(new {result = response});
		            }

		            return Json(new {result = "Siri is in break mode. Try to be more polite!"});
		        }
				
		        return Json(new {result = "Sorry, we can't handle your message!"});
		    }
		    return Json(new {result = "Please, turn on your Dialogue Web Service"} );
        }

        [HttpGet]
		public JsonResult Handle(string message)
		{
            return Json(new {result = "Recieved " + message});
        }
    }

}