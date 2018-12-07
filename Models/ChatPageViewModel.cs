using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dialogue.Web.Models;

namespace Dialogue.Models
{
    public class ChatPageViewModel
    {
        public string UserName { get; set; }
        public List<Message> ChatHistory { get; set; }
    }
}
