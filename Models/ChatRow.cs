using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialogue.Models
{
    public class ChatRow
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
