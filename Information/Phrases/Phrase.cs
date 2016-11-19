using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases
{
    public class Phrase
    {
        public string Subject { get; set; }

        public string Verb { get; set; }

        public string Object { get; set; }

        public string Adjective { get; set; }

        public Phrase()
        {
            Subject = string.Empty;
            Verb = string.Empty;
            Object = string.Empty;
            Adjective = string.Empty;
        }
    }
}
