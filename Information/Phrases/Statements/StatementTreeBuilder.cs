using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Statements
{
    public static class StatementTreeBuilder
    {
        public static StatementNode BuildNewTree(StatementType statementType)
        {
            StatementNode tree;

            switch (statementType)
            {
                case StatementType.Did:
                    tree = BuildDidTree();
                    break;

                case StatementType.DidNot:
                    tree = BuildDidNotTree();
                    break;

                case StatementType.Is:
                    tree = BuildIsTree();
                    break;

                case StatementType.IsNot:
                    tree = BuildIsNotTree();
                    break;

                default:
                    throw new NotImplementedException();
            }

            tree.StatementType = statementType;
            return tree;
        }

        private static StatementNode BuildDidTree()
        {
            var subject = new StatementNode("<subject>", WordType.Subject, Tense.Past);
            subject.IsPlaceholder = true;

            var verb = new StatementNode("<verbed>", WordType.Verb, Tense.Past);
            verb.IsPlaceholder = true;

            var obj = new StatementNode("<object>", WordType.Object, Tense.Past);
            obj.IsPlaceholder = true;
            obj.StatementType = StatementType.Did;

            verb.AddNode(obj);
            subject.AddNode(verb);

            return subject;
        }

        private static StatementNode BuildDidNotTree()
        {
            var subject = new StatementNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var didNot = new StatementNode("did not", WordType.Other, Tense.Base);

            var verb = new StatementNode("<verb>", WordType.Verb, Tense.Base);
            verb.IsPlaceholder = true;

            var obj = new StatementNode("<object>", WordType.Object, Tense.Base);
            obj.IsPlaceholder = true;

            verb.AddNode(obj);
            didNot.AddNode(verb);
            subject.AddNode(didNot);

            return subject;
        }

        private static StatementNode BuildIsTree()
        {
            var subject = new StatementNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var isWord = new StatementNode("is", WordType.Other, Tense.Base);

            var adjective = new StatementNode("<adjective>", WordType.Adjective, Tense.Base);
            adjective.IsPlaceholder = true;
            adjective.StatementType = StatementType.Is;

            isWord.AddNode(adjective);
            subject.AddNode(isWord);

            return subject;
        }

        private static StatementNode BuildIsNotTree()
        {
            var subject = new StatementNode("<subject>", WordType.Subject, Tense.Base);
            subject.IsPlaceholder = true;

            var isWord = new StatementNode("is", WordType.Other, Tense.Base);

            var not = new StatementNode("not", WordType.Other, Tense.Base);

            var adjective = new StatementNode("<adjective>", WordType.Adjective, Tense.Base);
            adjective.IsPlaceholder = true;
            adjective.StatementType = StatementType.IsNot;

            not.AddNode(adjective);
            isWord.AddNode(not);
            subject.AddNode(isWord);

            return subject;
        }
    }
}
