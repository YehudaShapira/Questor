using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Statements
{
    public class StatementTree : PhraseTree
    {
        public StatementTree(StatementNode root)
            : base(root)
        {

        }

        public Statement GetSelectedStatement()
        {
            var phrase = GetSelectedPhrase();
            var statementNode = CurrentNode as StatementNode;

            var statement = new Statement(phrase, statementNode.StatementType);

            return statement;
        }
    }
}
