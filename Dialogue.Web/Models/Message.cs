using System;
using Dialogue.Models;
namespace Dialogue.Web.Models
{
    public class Message
    {
        public int Id { get; private set; }

        public int UserId { get; set; }

        public AuthorId Author { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
    
}