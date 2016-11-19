using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Statements
{
    public class Statement : Phrase
    {
        public StatementType StatementType { get; set; }

        public Statement() : base()
        {

        }

        public Statement(Phrase phrase, StatementType statementType)
        {
            this.Subject = phrase.Subject;
            this.Verb = phrase.Verb;
            this.Object = phrase.Object;
            this.Adjective = phrase.Adjective;

            this.StatementType = statementType;
        }
    }
}
