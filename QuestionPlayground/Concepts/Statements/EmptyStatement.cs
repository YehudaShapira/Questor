using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts.Statements
{
    public class EmptyStatement : Statement
    {
        public EmptyStatement()
        {

        }

        public override bool IsConsistent(Statement other)
        {
            return true;
        }
    }
}
