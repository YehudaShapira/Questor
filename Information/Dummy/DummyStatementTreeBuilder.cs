using Questor.Inquiry.Phrases;
using Questor.Inquiry.Phrases.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Dummy
{
    public static class DummyStatementTreeBuilder
    {
        public static StatementTree BuildTree()
        {
            var root = new StatementNode(string.Empty, WordType.Other, Tense.Base);

            var suspects = new List<string>
            {
                "Miss Scarlet", "Professor Plum", "Mrs. Peacock", "Mr. Green", "Colonel Mustard", "Mrs. White"
            };

            BuildAttributeTrees(root);
            BuildDidNotTree(root);
            BuildDidTree(root);

            foreach (var suspect in suspects)
            {
                root.AddSubject(suspect);
            }

            return new StatementTree(root);
        }

        private static void BuildAttributeTrees(StatementNode root)
        {
            var adjectives = new List<string>
            {
                "tall", "evil", "bemustached", "a total slob"
            };

            var isRoot = StatementTreeBuilder.BuildNewTree(StatementType.Is);
            var isNotRoot = StatementTreeBuilder.BuildNewTree(StatementType.IsNot);
            foreach (var adjective in adjectives)
            {
                isRoot.AddAdjective(adjective);
                isNotRoot.AddAdjective(adjective);
            }

            isRoot.StatementType = StatementType.Is;
            isNotRoot.StatementType = StatementType.IsNot;

            root.AddNode(isNotRoot);
            root.AddNode(isRoot);
        }

        private static void BuildDidTree(StatementNode root)
        {
            var didRoot = StatementTreeBuilder.BuildNewTree(StatementType.Did);

            didRoot.AddVerb("stab", "stabbed");
            didRoot.AddVerb("shoot", "shot");
            didRoot.AddVerb("strangle", "strangled");
            didRoot.AddVerb("maul", "mauled");
            didRoot.AddVerb("poison", "poisoned");
            didRoot.AddVerb("throw a piano on", "threw a piano on");

            didRoot.AddObject("the cat");
            didRoot.AddObject("Mr. Mulberry");
            didRoot.AddObject("Roger Rabbit");
            didRoot.AddObject("Mr. Boddy");

            didRoot.StatementType = StatementType.Did;
            
            root.AddNode(didRoot);
        }

        private static void BuildDidNotTree(StatementNode root)
        {
            var didNotRoot = StatementTreeBuilder.BuildNewTree(StatementType.DidNot);

            didNotRoot.AddVerb("stab", "stabbed");
            didNotRoot.AddVerb("shoot", "shot");
            didNotRoot.AddVerb("strangle", "strangled");
            didNotRoot.AddVerb("maul", "mauled");
            didNotRoot.AddVerb("poison", "poisoned");
            didNotRoot.AddVerb("throw a piano on", "threw a piano on");

            didNotRoot.AddObject("the cat");
            didNotRoot.AddObject("Mr. Mulberry");
            didNotRoot.AddObject("Roger Rabbit");
            didNotRoot.AddObject("Mr. Boddy");

            didNotRoot.StatementType = StatementType.DidNot;

            root.AddNode(didNotRoot);
        }
    }
}
