using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Statements
{
    public class StatementNode : PhraseNode
    {
        private StatementType statementType;

        /// <summary>
        /// Question type of the entire tree
        /// </summary>
        public StatementType StatementType
        {
            get
            {
                return statementType;
            }
            set
            {
                statementType = value;

                // Not really necessary, but whole tree might as well know what kind it is.
                foreach (var node in Next)
                {
                    var statementNode = node as StatementNode;
                    statementNode.StatementType = value;
                }
            }
        }

        public StatementNode(string word, WordType wordType, Tense tense)
            : base(word, wordType, tense)
        { }

        public StatementNode(string word, WordType wordType, StatementType statementType, Tense tense)
            : base(word, wordType, tense)
        {
            StatementType = statementType;
        }

        protected override PhraseNode DuplicateBasic()
        {
            return new StatementNode(this.BaseWord, this.WordType, this.StatementType, this.Tense);
        }
    }
}
