using Questor.Inquiry.Phrases;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data.Info
{
	public class AttributeInformation : Information
	{
		public string Adjective { get; set; }

		public AttributeInformation()
		{
			Adjective = string.Empty;
		}

		public override bool IsRelevant(Phrase phrase)
		{
			return
				Subject.Equals(phrase.Subject, StringComparison.InvariantCultureIgnoreCase) &&
				Adjective.Equals(phrase.Adjective, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
