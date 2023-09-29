using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotXML.Models
{
    public class Sentiment
    {
        public string Title { get; set; }
        public List<Message> Messages { get; set; }
    }
}
