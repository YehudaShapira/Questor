using Questor.Inquiry.Data.Info;
using Questor.Inquiry.Phrases.Questioning;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data
{
    public class BeliefSet
    {
        private Dictionary<Information, Metadata> metadata;

        private List<EventInformation> events;
        private List<AttributeInformation> attributes;

        public BeliefSet()
        {
            metadata = new Dictionary<Information, Metadata>();

            events = new List<EventInformation>();
            attributes = new List<AttributeInformation>();
        }

        internal void AddEvent(EventInformation info)
        {
            events.Add(info);
        }

        internal void AddSubjectAttribute(AttributeInformation info)
        {
            attributes.Add(info);
        }

        public Answer Answer(Question question)
        {
            IEnumerable<Information> matchingInfo;

            switch (question.QuestionType)
            {
                case QuestionType.Did:
                    matchingInfo = events.Where(x =>
                           x.Subject.Equals(question.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Verb.Equals(question.Verb, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Object.Equals(question.Object, StringComparison.InvariantCultureIgnoreCase));
                    break;

                case QuestionType.Is:
                    matchingInfo = attributes.Where(x =>
                         x.Subject.Equals(question.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                         x.Adjective.Equals(question.Adjective, StringComparison.InvariantCultureIgnoreCase));
                    break;

                default:
                    throw new NotImplementedException();
            }

            if (matchingInfo.Any())
            {
                var beliefStrength = CalculateBeliefStrength(matchingInfo);

                if (beliefStrength > 0)
                {
                    return new Answer { AnswerType = AnswerType.Yes };
                }
                else if (beliefStrength < 0)
                {
                    return new Answer { AnswerType = AnswerType.No };
                }
                else
                {
                    return new Answer { AnswerType = AnswerType.NotKnown };
                }
            }
            else
            {
                return new Answer { AnswerType = AnswerType.NotKnown };
            }
        }

        private static int CalculateBeliefStrength(IEnumerable<Information> matchingInfo)
        {
            var trueInfo = matchingInfo.FirstOrDefault(x => x.IsTrue);
            var beliefStrength = 0;
            if (trueInfo != null)
            {
                beliefStrength = trueInfo.Metadata.BeliefStrength;
            }

            var falseInfo = matchingInfo.FirstOrDefault(x => !x.IsTrue);
            var disbeliefStrength = 0;
            if (falseInfo != null)
            {
                disbeliefStrength = falseInfo.Metadata.BeliefStrength;
            }

            beliefStrength -= disbeliefStrength;
            return beliefStrength;
        }

        public BeliefResponse Accept(Statement statement, int credibility, string source)
        {
            var response = new BeliefResponse();

            var matching = GetMatching(statement);

            if (matching.Any())
            {
                response.IsNewBelief = false;

                foreach (var info in matching)
                {
                    if (!info.Metadata.Sources.Contains(source))
                    {
                        info.Metadata.Sources.Add(source);
                        info.Metadata.BeliefStrength += credibility;
                    }
                }
            }
            else
            {
                response.IsNewBelief = true;
                AddNewInformation(statement, credibility, source);
            }

            var matchingAfterStatement = GetMatching(statement);
            response.BeliefStrength = matchingAfterStatement.Sum(x => x.Metadata.BeliefStrength);

            return response;
        }

        private void AddNewInformation(Statement statement, int credibility, string source)
        {
            switch (statement.StatementType)
            {
                case StatementType.Did:
                    events.Add(new EventInformation
                    {
                        Subject = statement.Subject,
                        Verb = statement.Verb,
                        Object = statement.Object,
                        IsTrue = true,
                        Metadata = new Metadata
                        {
                            BeliefStrength = credibility,
                            Sources = new List<string> { source }
                        }
                    });
                    break;

                case StatementType.DidNot:
                    events.Add(new EventInformation
                    {
                        Subject = statement.Subject,
                        Verb = statement.Verb,
                        Object = statement.Object,
                        IsTrue = false,
                        Metadata = new Metadata
                        {
                            BeliefStrength = credibility,
                            Sources = new List<string> { source }
                        }
                    });
                    break;

                case StatementType.Is:
                    attributes.Add(new AttributeInformation
                    {
                        Subject = statement.Subject,
                        Adjective = statement.Adjective,
                        IsTrue = true,
                        Metadata = new Metadata
                        {
                            BeliefStrength = credibility,
                            Sources = new List<string> { source }
                        }
                    });
                    break;

                case StatementType.IsNot:
                    attributes.Add(new AttributeInformation
                    {
                        Subject = statement.Subject,
                        Adjective = statement.Adjective,
                        IsTrue = false,
                        Metadata = new Metadata
                        {
                            BeliefStrength = credibility,
                            Sources = new List<string> { source }
                        }
                    });
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public IEnumerable<Information> GetMatching(Statement statement)
        {
            switch (statement.StatementType)
            {
                case StatementType.Did:
                    return events.Where(x =>
                           x.Subject.Equals(statement.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Verb.Equals(statement.Verb, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Object.Equals(statement.Object, StringComparison.InvariantCultureIgnoreCase) &&
                           x.IsTrue)
                           .Cast<Information>();

                case StatementType.DidNot:
                    return events.Where(x =>
                           x.Subject.Equals(statement.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Verb.Equals(statement.Verb, StringComparison.InvariantCultureIgnoreCase) &&
                           x.Object.Equals(statement.Object, StringComparison.InvariantCultureIgnoreCase) &&
                           !x.IsTrue)
                           .Cast<Information>();

                case StatementType.Is:
                    return attributes.Where(x =>
                         x.Subject.Equals(statement.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                         x.Adjective.Equals(statement.Adjective, StringComparison.InvariantCultureIgnoreCase) &&
                         x.IsTrue)
                         .Cast<Information>();

                case StatementType.IsNot:
                    return attributes.Where(x =>
                         x.Subject.Equals(statement.Subject, StringComparison.InvariantCultureIgnoreCase) &&
                         x.Adjective.Equals(statement.Adjective, StringComparison.InvariantCultureIgnoreCase) &&
                         !x.IsTrue)
                         .Cast<Information>();

                default:
                    throw new NotImplementedException();
            }
        }

        public IEnumerable<Information> GetCondradictions(Statement statement)
        {
            var matching = GetMatching(statement);

            switch (statement.StatementType)
            {
                case StatementType.Did:
                case StatementType.Is:
                    return matching.Where(x => x.IsTrue == false);

                case StatementType.DidNot:
                case StatementType.IsNot:
                    return matching.Where(x => x.IsTrue == true);

                default:
                    throw new NotImplementedException();
            }
        }

        public void AddCredibility(int credibility, string source)
        {
            foreach (var info in events)
            {
                if (info.Metadata.Sources.Contains(source))
                {
                    info.Metadata.BeliefStrength += credibility;
                }
            }
            foreach (var info in attributes)
            {
                if (info.Metadata.Sources.Contains(source))
                {
                    info.Metadata.BeliefStrength += credibility;
                }
            }
        }

        public void RemoveCredibility(int credibility, string source)
        {
            AddCredibility(-credibility, source);
        }
    }
}
