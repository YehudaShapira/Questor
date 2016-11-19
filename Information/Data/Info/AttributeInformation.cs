using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data.Info
{
    public class AttributeInformation : Information
    {
        public string Adjective { get; set; }

        public AttributeInformation()
        {
            Adjective = string.Empty;
        }
    }
}
