using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.ConsoleGame
{
    public class StatementChooser : PhraseChooser
    {
        protected override string PhraseEnd
        {
            get
            {
                return ".";
            }
        }

        public StatementChooser(StatementTree tree, int left, int top)
            : base(tree, left, top)
        {

        }

        public Statement GetPlayerChoice()
        {
            var res = new Statement();
            ShowRoot();

            while (tree.NextNodes.Any())
            {
                GetNextSelection();
            }

            var statementTree = tree as StatementTree;
            res = statementTree.GetSelectedStatement();
            tree.ResetSelection();

            return res;
        }
    }
}
