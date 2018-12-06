using Dialogue.Services;
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
		public JsonResult HandleMessage(string message)
		{
            var response = _chatService.responseToMessage(message);   
            return Json(new {result = response});
        }

        [HttpGet]
		public JsonResult Handle(string message)
		{
            return Json(new {result = "Recieved " + message});
        }
    }

}