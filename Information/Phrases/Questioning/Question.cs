using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Questioning
{
    public class Question : Phrase
    {
        public QuestionType QuestionType { get; set; }

        public Question() : base()
        {

        }

        public Question(Phrase phrase, QuestionType questionType)
        {
            this.Subject = phrase.Subject;
            this.Verb = phrase.Verb;
            this.Object = phrase.Object;
            this.Adjective = phrase.Adjective;

            this.QuestionType = questionType;
        }
    }
}
