﻿

using Dialogue.Web.Data;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Dialogue.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private DialogueAppContext _db;
        private IPasswordHasher<User> _passwordHasher;

        public UserApiController(DialogueAppContext db, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        [Route(""), HttpPost]
        public IActionResult AddUser([FromBody] RegisterUserDto userDto)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserName == userDto.UserName);

             if (user != null)
                return BadRequest("User with this username already exist.");

            User newUser = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            string hashedPassword = _passwordHasher.HashPassword(newUser, userDto.PasswordString);
            newUser.Password = hashedPassword;

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return Ok();
        }

        [Route("login"), HttpPost]
        public IActionResult UserLogin([FromBody] UserDto userDto)
        {
            
            User user = _db.Users.FirstOrDefault(u => u.UserName == userDto.UserName);

             if (user == null)
                return NotFound();

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userDto.PasswordString);
            if(result == PasswordVerificationResult.Success)
                return Ok();
            
            return BadRequest("Password doesn't match");
        }


    }

    public class UserDto
    {
        public string UserName { get; set; }

        public string PasswordString { get; set; }
    }

    public class RegisterUserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordString { get; set; }

        public string FirstName {get; set; }

        public string LastName {get; set; }
    }
}
