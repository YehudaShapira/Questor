using Bdi.Objects;
using Bdi.Objects.Attribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts.Statements
{
    public class StaticStatement : Statement
    {
        public string SubjectName { get; set; }

        public NounAttribute Attribute { get; set; }

        private StaticStatement()
        {

        }

        public StaticStatement(string subjectName, NounAttribute attribute)
        {
            SubjectName = subjectName;
            Attribute = attribute;
        }

        public override bool IsConsistent(Statement other)
        {
            if (other is CompoundStatement)
            {
                var compound = other as CompoundStatement;
                return compound.IsConsistent(this);
            }
            else if (other is DynamicStatement)
            {
                return true;
            }
            else if (other is StaticStatement)
            {
                var otherStatic = other as StaticStatement;

                bool sameSubject = (this.SubjectName == otherStatic.SubjectName);
                bool sameAttribute = (this.Attribute.Key == otherStatic.Attribute.Key);
                bool sameValue = (this.Attribute.Value == otherStatic.Attribute.Value);

                if (sameSubject && sameAttribute && !sameValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (other is EmptyStatement)
            {
                return true;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return SubjectName + " " + Attribute.Key + " is " + Attribute.Value;
        }
    }
}
