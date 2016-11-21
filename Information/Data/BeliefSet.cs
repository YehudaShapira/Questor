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
			if (IsYesNoQuestion(question))
			{
				return AnswerYesOrNo(question);
			}
			else if (question.QuestionType == QuestionType.WhoDid)
			{
				var who = new List<string>();

				IEnumerable<Information> relevantInfo;
				relevantInfo = events.Where(x =>
					x.Verb == question.Verb &&
					x.Object == question.Object);

				var did = relevantInfo.Where(x => x.IsTrue);
				var didNot = relevantInfo.Where(x => !x.IsTrue);

				foreach (var d in did)
				{
					var contradicting = didNot.FirstOrDefault(n => n.Subject.Equals(d.Subject, StringComparison.InvariantCultureIgnoreCase));
					if (contradicting == null ||
						d.Metadata.BeliefStrength > contradicting.Metadata.BeliefStrength)
					{
						who.Add(d.Subject);
					}
				}

				if (who.Any())
				{
					return new Answer
					{
						AnswerType = AnswerType.People,
						People = who
					};
				}
				else
				{
					return new Answer { AnswerType = AnswerType.NotKnown };
				}
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		private bool IsYesNoQuestion(Question question)
		{
			return
				question.QuestionType == QuestionType.AreYou ||
				question.QuestionType == QuestionType.Did ||
				question.QuestionType == QuestionType.Does ||
				question.QuestionType == QuestionType.DoYou ||
				question.QuestionType == QuestionType.Is;
		}

		private Answer AnswerYesOrNo(Question question)
		{
			IEnumerable<Information> relevantInfo;

			switch (question.QuestionType)
			{
				case QuestionType.Did:
					relevantInfo = events.Where(x => x.IsRelevant(question));
					break;

				case QuestionType.Is:
					relevantInfo = attributes.Where(x => x.IsRelevant(question));
					break;

				default:
					throw new NotImplementedException();
			}

			if (relevantInfo.Any())
			{
				var truthStrength = CalculateTruthStrength(relevantInfo);

				if (truthStrength > 0)
				{
					return new Answer { AnswerType = AnswerType.Yes };
				}
				else if (truthStrength < 0)
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

		private static int CalculateTruthStrength(IEnumerable<Information> matchingInfo)
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

				// For most information types, there can be only one match
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

			var matchingAfterStatement = GetMatching(statement).Sum(x => x.Metadata.BeliefStrength);
			var contradicting = GetCondradicting(statement).Sum(x => x.Metadata.BeliefStrength);
			response.BeliefStrength = matchingAfterStatement - contradicting;

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
					return events.Where(x => x.IsRelevant(statement) && x.IsTrue)
						.Cast<Information>();

				case StatementType.DidNot:
					return events.Where(x => x.IsRelevant(statement) && !x.IsTrue)
						.Cast<Information>();

				case StatementType.Is:
					return attributes.Where(x => x.IsRelevant(statement) && x.IsTrue)
						.Cast<Information>();

				case StatementType.IsNot:
					return attributes.Where(x => x.IsRelevant(statement) && !x.IsTrue)
						.Cast<Information>();

				default:
					throw new NotImplementedException();
			}
		}

		public IEnumerable<Information> GetCondradicting(Statement statement)
		{
			switch (statement.StatementType)
			{
				case StatementType.Did:
					return events.Where(x => x.IsRelevant(statement) && !x.IsTrue)
						.Cast<Information>();

				case StatementType.DidNot:
					return events.Where(x => x.IsRelevant(statement) && x.IsTrue)
						.Cast<Information>();

				case StatementType.Is:
					return attributes.Where(x => x.IsRelevant(statement) && !x.IsTrue)
						.Cast<Information>();

				case StatementType.IsNot:
					return attributes.Where(x => x.IsRelevant(statement) && x.IsTrue)
						.Cast<Information>();

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
