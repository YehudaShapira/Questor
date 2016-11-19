using Questor.Inquiry.Data.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Questor.Inquiry.Data
{
    public static class BeliefSetGenerator
    {
        public static BeliefSet GenerateFromXml(XmlDocument document)
        {
            var res = new BeliefSet();

            GetEvents(document, res);
            GetAttributes(document, res);

            return res;
        }

        private static void GetEvents(XmlDocument document, BeliefSet res)
        {
            var events = document.SelectSingleNode("Beliefs/Events");
            foreach (XmlNode node in events.ChildNodes)
            {
                var info = new EventInformation()
                {
                    Subject = node.SelectSingleNode("Subject").InnerText,
                    Verb = node.SelectSingleNode("Verb").InnerText,
                    Object = node.SelectSingleNode("Object").InnerText,
                    IsTrue = XmlConvert.ToBoolean(node.SelectSingleNode("IsTrue").InnerText),

                    Reason = node.SelectSingleNode("Reason").InnerText,
                    Time = node.SelectSingleNode("Time").InnerText,
                    Place = node.SelectSingleNode("Place").InnerText
                };
                res.AddEvent(info);
            }
        }

        private static void GetAttributes(XmlDocument document, BeliefSet res)
        {
            /*var events = document.SelectSingleNode("Beliefs/Attributes");
            foreach (XmlNode node in events.ChildNodes)
            {
                var isList = new List<string>();
                var isntList = new List<string>();
                foreach (XmlNode attribute in node.SelectSingleNode("Is").ChildNodes)
                {
                    isList.Add(attribute.InnerText);
                }
                foreach (XmlNode attribute in node.SelectSingleNode("IsNot").ChildNodes)
                {
                    isntList.Add(attribute.InnerText);
                }

                var info = new AttributeInformation()
                {
                    Subject = node.SelectSingleNode("Subject").InnerText,
                    Is = isList,
                    IsNot = isntList
                };
                res.AddSubjectAttribute(info);
            }*/
            throw new NotImplementedException();
        }
    }
}
