using Questor.Inquiry.Phrases.Questioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.ConsoleGame
{
    public class QuestionChooser : PhraseChooser
    {
        protected override string PhraseEnd
        {
            get
            {
                return "?";
            }
        }

        public QuestionChooser(QuestionTree tree, int left, int top)
            : base(tree, left, top)
        {

        }

        public Question GetPlayerChoice()
        {
            var res = new Question();
            ShowRoot();

            while (tree.NextNodes.Any())
            {
                GetNextSelection();
            }

            var questionTree = tree as QuestionTree;
            res = questionTree.GetSelectedQuestion();
            tree.ResetSelection();

            return res;
        }
    }
}
