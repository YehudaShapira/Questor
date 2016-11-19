using Bdi.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts.Statements
{
    public class DynamicStatement : Statement
    {
        public SingleAction Action { get; set; }

        public Statement Result { get; set; }

        public override bool IsConsistent(Statement other)
        {
            if (other is CompoundStatement)
            {
                var compound = other as CompoundStatement;
                return compound.IsConsistent(this);
            }
            else if (other is DynamicStatement)
            {
                var otherDynamic = other as DynamicStatement;

                bool sameAction = this.Action.IsSameAction(otherDynamic.Action);
                bool consistentResult = this.Result.IsConsistent(otherDynamic.Result);

                if (sameAction && !consistentResult)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (other is StaticStatement)
            {
                return true;
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
            return Action.ToString() + " would result in " + Result.ToString();
        }
    }
}
