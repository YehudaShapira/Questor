using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts.Queries
{
    public class AttributeQuestion : Question
    {
        public string SubjectName { get; set; }
        public string Attribute { get; set; }
    }
}
