using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Questioning
{
    public class QuestionNode : PhraseNode
    {
        private QuestionType questionType;

        /// <summary>
        /// Question type of the entire tree
        /// </summary>
        public QuestionType QuestionType
        {
            get
            {
                return questionType;
            }
            set
            {
                questionType = value;

                // Not really necessary, but whole tree might as well know what kind it is.
                foreach (var node in Next)
                {
                    var questionNode = node as QuestionNode;
                    questionNode.QuestionType = value;
                }
            }
        }

        public QuestionNode(string word, WordType wordType, Tense tense)
            : base(word, wordType, tense)
        { }

        public QuestionNode(string word, WordType wordType, QuestionType questionType, Tense tense)
            : base(word, wordType, tense)
        {
            QuestionType = questionType;
        }

        protected override PhraseNode DuplicateBasic()
        {
            return new QuestionNode(this.BaseWord, this.WordType, this.QuestionType, this.Tense);
        }
    }
}
