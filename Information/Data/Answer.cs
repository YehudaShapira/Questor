using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Data
{
    public class Answer
    {
        public AnswerType AnswerType { get; set; }

        public string Time { get; set; }
        public string Place { get; set; }
        public string Reason { get; set; }

        public Answer()
        {
            Time = string.Empty;
            Place = string.Empty;
            Reason = string.Empty;
        }

        public override string ToString()
        {
            switch (this.AnswerType)
            {
                case AnswerType.Yes:
                    return "Yes.";
                case AnswerType.No:
                    return "No.";
                case AnswerType.Time:
                    return Time + ".";
                case AnswerType.Place:
                    return Place + ".";
                case AnswerType.Reason:
                    return Reason + ".";
                case AnswerType.WrongAssumption:
                    return "You are making a wrong assumption...";
                case AnswerType.NotKnown:
                    return "I don't know.";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
