using Dialogue.Web.Data;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialogue.Web.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private DialogueAppContext _db;

        public MessageApiController(DialogueAppContext db)
        {
            _db = db;
        }

        [Route("{userName}"), HttpPost]
        public async Task<IActionResult> AddMessage([FromRoute] string userName, [FromBody] MessageDto messageDto)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                return NotFound();

            Message message = new Message
            {
                UserId = user.Id,
                Author = messageDto.Author,
                Time = DateTime.Now,
                Text = messageDto.Text
            };

            await _db.Messages.AddAsync(message);
            _db.SaveChanges();

            return Ok();
        }

        [Route("{userName}/all"), HttpPost]
        public IActionResult GetPreviousMessages([FromRoute] string userName)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                return NotFound();

            List<Message> messages = _db.Messages.Where(m => m.UserId == user.Id).ToList();

            return Ok(messages);
        }

    }

    public class MessageDto
    {
        public string Text { get; set; }
        public AuthorId Author { get; set; }
    }
}
