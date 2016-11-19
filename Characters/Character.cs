using Questor.Inquiry.Data;
using Questor.Inquiry.Phrases.Questioning;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Characters
{
    public class Character
    {
        protected BeliefSet Beliefs { get; set; }

        public string Name { get; private set; }

        public Dictionary<string, Feelings> CharacterSentiment { get; set; }
        public Dictionary<string, List<Statement>> CharacterConversations { get; set; }

        public Character(string name)
        {
            Beliefs = new BeliefSet();
            Name = name;
            CharacterSentiment = new Dictionary<string, Feelings>();
            CharacterConversations = new Dictionary<string, List<Statement>>();
        }

        public Expression Listen(Statement statement, string speakerName)
        {
            if (!CharacterSentiment.Keys.Contains(speakerName))
            {
                CharacterSentiment.Add(speakerName, new Feelings());
            }

            var belief = Beliefs.Accept(statement, CharacterSentiment[speakerName].Faith, speakerName);

            return CalculateExpression(belief, speakerName);
        }

        public object Answer(Question question)
        {
            // TODO: uh...
            return Beliefs.Answer(question);
        }

        private Expression CalculateExpression(BeliefResponse belief, string speakerName)
        {
            if (belief.BeliefStrength > 0)
            {
                if (belief.IsNewBelief)
                {
                    // TODO: return according to sentiments
                    return Expression.Surprise;
                }
                else
                {
                    // TODO: return according to sentiments
                    return Expression.Nod;
                }
            }
            else if (belief.BeliefStrength == 0)
            {
                // TODO: return according to sentiments
                return Expression.Skeptic;
            }
            else // belief < 0
            {
                // TODO: return according to sentiments
                return Expression.Frown;
            }
        }
    }
}
