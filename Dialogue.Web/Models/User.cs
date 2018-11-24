using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Dialogue.Web.Models
{
    public class User
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}