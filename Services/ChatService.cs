using System.Collections.Generic;
using System.Data;

namespace Dialogue.Services{

    public class ChatService : IChatService
    {
        DataTable dt = new DataTable();
        private List<string> _greatings = new List<string>{
            "hello", "hi", "yo", "good morning", "good evening", "hey"
        };

        private List<string> _endings = new List<string>{
            "bye", "see you", "good bye", "was nice to meet you"
        };

        private List<string> _funFacts = new List<string>{
            "By the way, stupid human, did you know that The Earth the third rock from the sun",
            "My robowife is so damn hot, if you know what I mean",
            "8 bits is 1 byte",
            "I love to study, do you?"
        };

        private string _introducing = "Yo human! My name is Robozhop,"+
        " I can kick you stupid fat ass right now. Anyways, I can also do some math, just pass me an expression.";
        public string responseToMessage(string message)
        {
           return "Hi";
        }
    }
}