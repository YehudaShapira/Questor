using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Actions
{
    public class SingleAction
    {
        public string Name { get; set; }

        public List<string> Arguments { get; set; }

        public bool IsSameAction(SingleAction other)
        {
            throw new NotImplementedException();
        }
    }
}
