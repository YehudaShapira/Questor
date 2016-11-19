using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Questioning
{
    public static class QuestionTreeBuilder
    {
        public static QuestionNode BuildNewTree(QuestionType questionType)
        {
            QuestionNode tree;

            switch (questionType)
            {
                case QuestionType.Did:
                    tree =  BuildDidTree();
                    break;

                case QuestionType.Does:
                    tree = BuildDoesTree();
                    break;

                case QuestionType.DoYou:
                    tree = BuildDoYouTree();
                    break;

                case QuestionType.Is:
                    tree = BuildIsTree();
                    break;

                case QuestionType.AreYou:
                    tree = BuildAreYouTree();
                    break;

                case QuestionType.WhenDid:
                    tree = BuildWhenDidTree();
                    break;

                case QuestionType.WhereDid:
                    tree = BuildWhereDidTree();
                    break;

                case QuestionType.WhereIs:
                    tree = BuildWhereIsTree();
                    break;

                case QuestionType.WhoDid:
                    tree = BuildWhoDidTree();
                    break;

                case QuestionType.WhoDoes:
                    tree = BuildWhoDoesTree();
                    break;

                case QuestionType.WhoIs:
                    tree = BuildWhoIsTree();
                    break;

                case QuestionType.WhyDid:
                    tree = BuildWhyDidTree();
                    break;

                default:
                    throw new NotImplementedException();
            }

            tree.QuestionType = questionType;
            return tree;
        }

        private static QuestionNode BuildDidTree()
        {
            var did = new QuestionNode("Did", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            subject.AddNode(verb);
            did.AddNode(subject);

            return did;
        }

        private static QuestionNode BuildDoesTree()
        {
            var does = new QuestionNode("Does", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            subject.AddNode(verb);
            does.AddNode(subject);

            return does;
        }

        private static QuestionNode BuildDoYouTree()
        {
            var doYou = new QuestionNode("Do you", WordType.Other, Tense.Base);

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            doYou.AddNode(verb);

            return doYou;
        }

        private static QuestionNode BuildIsTree()
        {
            var isTree = new QuestionNode("Is", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var adjective = new QuestionNode("<adjective>", WordType.Adjective, Tense.Base);
            adjective.IsPlaceholder = true;

            subject.AddNode(adjective);
            isTree.AddNode(subject);

            return isTree;
        }

        private static QuestionNode BuildAreYouTree()
        {
            var areYou = new QuestionNode("Are you", WordType.Other, Tense.Base);

            var adjective = new QuestionNode("<adjective>", WordType.Adjective, Tense.Base);
            adjective.IsPlaceholder = true;

            areYou.AddNode(adjective);

            return areYou;
        }

        private static QuestionNode BuildWhenDidTree()
        {
            var whenDid = new QuestionNode("When did", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            subject.AddNode(verb);
            whenDid.AddNode(subject);

            return whenDid;
        }

        private static QuestionNode BuildWhereDidTree()
        {
            var whereDid = new QuestionNode("Where did", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            subject.AddNode(verb);
            whereDid.AddNode(subject);

            return whereDid;
        }

        private static QuestionNode BuildWhereIsTree()
        {
            var whereIs = new QuestionNode("Where is", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            whereIs.AddNode(subject);

            return whereIs;
        }

        private static QuestionNode BuildWhoDidTree()
        {
            var whoDid = new QuestionNode("Who", WordType.Other, Tense.Past);

            var verb = new QuestionNode("<verbed>", WordType.Verb, Tense.Past);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Past);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            whoDid.AddNode(verb);

            return whoDid;
        }

        private static QuestionNode BuildWhoDoesTree()
        {
            var whoDoes = new QuestionNode("Who", WordType.Other, Tense.Progressive);

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Progressive);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Progressive);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            whoDoes.AddNode(verb);

            return whoDoes;
        }

        private static QuestionNode BuildWhoIsTree()
        {
            var whoIs = new QuestionNode("Who is", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            whoIs.AddNode(subject);

            return whoIs;
        }

        private static QuestionNode BuildWhyDidTree()
        {
            var whyDid = new QuestionNode("Why did", WordType.Other, Tense.Base);

            var subject = new QuestionNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var verb = new QuestionNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new QuestionNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            subject.AddNode(verb);
            whyDid.AddNode(subject);

            return whyDid;
        }
    }
}
