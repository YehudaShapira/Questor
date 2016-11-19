using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data.Info
{
    public class Metadata
    {
        public int BeliefStrength { get; set; }
        public List<string> Sources { get; set; }

        public Metadata()
        {
            Sources = new List<string>();
        }
    }
}
