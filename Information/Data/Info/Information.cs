using Questor.Inquiry.Phrases;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data.Info
{
	public abstract class Information
	{
		public string Subject { get; set; }
		public bool IsTrue { get; set; }
		public Metadata Metadata { get; set; }

		public Information()
		{
			Subject = string.Empty;
			Metadata = new Metadata();
		}

		public abstract bool IsRelevant(Phrase phrase);
	}
}
