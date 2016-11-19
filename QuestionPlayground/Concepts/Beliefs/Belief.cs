using Bdi.Concepts.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts
{
    public class Belief
    {
        public Statement Statement { get; set; }

        public int Strength { get; set; }

        private Belief()
        {

        }

        public Belief(Statement statment)
        {
            Statement = statment;
        }

        public override string ToString()
        {
            return Statement.ToString();
        }
    }
}
