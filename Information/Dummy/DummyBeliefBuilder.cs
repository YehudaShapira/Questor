using Questor.Inquiry.Data;
using Questor.Inquiry.Data.Info;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Questor.Inquiry.Dummy
{
    public static class DummyBeliefBuilder
    {
        public static BeliefSet BuildBeliefSet()
        {
            var res = new BeliefSet();

            BuildAttributeBeliefs(res);
            BuildEventBeliefs(res);

            return res;
        }

        private static void BuildAttributeBeliefs(BeliefSet res)
        {
            var suspects = new List<string>
            {
                "Miss Scarlet", "Professor Plum", "Mrs. Peacock", "Mr. Green", "Mrs. White"
            };

            var adjectives1 = new List<string>
            {
                "tall"
            };

            var adjectives2 = new List<string>
            {
                "evil", "bemustached"
            };

            foreach (var suspect in suspects)
            {
                

                foreach (var adjective in adjectives1)
                {
                    var info = new AttributeInformation()
                    {
                        Subject = suspect,
                        Adjective = adjective,
                        IsTrue = true
                    };
                    res.AddSubjectAttribute(info);
                }
                foreach (var adjective in adjectives2)
                {
                    var info = new AttributeInformation()
                    {
                        Subject = suspect,
                        Adjective = adjective,
                        IsTrue = false
                    };
                    res.AddSubjectAttribute(info);
                }
            }

            foreach (var adjective in adjectives2)
            {
                var info = new AttributeInformation()
                {
                    Subject = "Colonel Mustard",
                    Adjective = adjective,
                    IsTrue = true
                };
                res.AddSubjectAttribute(info);
            }
            foreach (var adjective in adjectives1)
            {
                var info = new AttributeInformation()
                {
                    Subject = "Colonel Mustard",
                    Adjective = adjective,
                    IsTrue = false
                };
                res.AddSubjectAttribute(info);
            }
        }

        private static void BuildEventBeliefs(BeliefSet res)
        {
            var suspects = new List<string>
            {
                "Miss Scarlet", "Professor Plum", "Mrs. Peacock", "Mr. Green", "Mrs. White"
            };

            var actions = new List<string>
            {
                "stab", "shoot", "strangle", "maul", "poison"
            };

            var victims = new List<string>
            {
                "the cat", "Mr. Mulberry", "Roger Rabbit", "Mr. Boddy"
            };

            var info = new EventInformation()
            {
                Subject = "Colonel Mustard",
                Verb = "throw a piano on",
                Object = "the cat",
                IsTrue = true,

                Reason = "Because he sure hated that cat",
                Time = "11 AM yesterday",
                Place = "In the living room"
            };

            res.AddEvent(info);

            foreach (var action in actions)
            {
                var falseInfo = new EventInformation()
                {
                    Subject = "Colonel Mustard",
                    Verb = action,
                    Object = "the cat",
                    IsTrue = false
                };

                res.AddEvent(falseInfo);
            }

            foreach (var suspect in suspects)
            {
                var falseInfo = new EventInformation()
                {
                    Subject = suspect,
                    Verb = "throw a piano on",
                    Object = "the cat",
                    IsTrue = false
                };

                res.AddEvent(falseInfo);
            }
        }
    }
}
