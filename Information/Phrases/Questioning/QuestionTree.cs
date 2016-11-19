using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Questioning
{
    public class QuestionTree : PhraseTree
    {
        public QuestionTree(QuestionNode root)
            : base(root)
        {

        }

        public Question GetSelectedQuestion()
        {
            var phrase = GetSelectedPhrase();
            var questionNode = CurrentNode as QuestionNode;

            var question = new Question(phrase, questionNode.QuestionType);

            return question;
        }
    }
}
