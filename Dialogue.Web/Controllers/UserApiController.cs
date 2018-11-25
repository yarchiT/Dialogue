

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
            User user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok();
        }

        [Route("login"), HttpPost]
        public IActionResult UserLogin([FromBody] UserDto userDto)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserName == userDto.UserName && u.Password == userDto.Password);

            if (user == null)
                return NotFound();

            return Ok();
        }


    }

    public class UserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
