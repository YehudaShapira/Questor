using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts.Statements
{
    public class CompoundStatement : Statement
    {
        public Statement First { get; set; }
        public Statement Second { get; set; }

        private CompoundStatement()
        {

        }

        public CompoundStatement(Statement first, Statement second)
        {
            if (!first.IsConsistent(second))
            {
                throw new Exception("Statements contradict");
            }

            First = first;
            Second = second;
        }

        public override bool IsConsistent(Statement other)
        {
            return First.IsConsistent(other) && Second.IsConsistent(other);
        }

        public override string ToString()
        {
            return First.ToString() + " and " + Second.ToString();
        }
    }
}
