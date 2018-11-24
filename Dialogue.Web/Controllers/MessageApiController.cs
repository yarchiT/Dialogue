using Dialogue.Web.Data;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [Route("{userId}"), HttpPost]
        public IActionResult AddStory([FromRoute] int storyId, [FromBody] MessageDto storyDto)
        {

            return Ok();
        }

    }

    public class MessageDto
    {
        public string Text { get; set; }
        public AuthorId Author { get; set; }
    }
}
