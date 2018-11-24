

using Dialogue.Web.Data;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Dialogue.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private DialogueAppContext _db;

        public UserApiController(DialogueAppContext db)
        {
            _db = db;
        }

        [Route(""), HttpPost]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
           
            return Ok();
        }

    }

    public class UserDto
    {
       
    }
}
