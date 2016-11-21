using Questor.Inquiry.Phrases;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data.Info
{
	public class EventInformation : Information
	{
		public string Verb { get; set; }
		public string Object { get; set; }
		public string Time { get; set; }
		public string Place { get; set; }
		public string Reason { get; set; }

		public EventInformation()
		{
			Verb = string.Empty;
			Object = string.Empty;
			Time = string.Empty;
			Place = string.Empty;
			Reason = string.Empty;
		}

		public override bool IsRelevant(Phrase phrase)
		{
			return
				Subject.Equals(phrase.Subject, StringComparison.InvariantCultureIgnoreCase) &&
				Verb.Equals(phrase.Verb, StringComparison.InvariantCultureIgnoreCase) &&
				Object.Equals(phrase.Object, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
