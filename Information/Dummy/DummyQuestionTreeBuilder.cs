using Questor.Inquiry.Phrases;
using Questor.Inquiry.Phrases.Questioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Dummy
{
    public static class DummyQuestionTreeBuilder
    {
        public static QuestionTree BuildTree()
        {
            var root = new QuestionNode(string.Empty, WordType.Other, Tense.Base);

            var isRoot = BuildIsTree();
            var didRoot = BuildDidTree();

            root.AddNode(isRoot);
            root.AddNode(didRoot);

            return new QuestionTree(root);
        }

        private static QuestionNode BuildIsTree()
        {
            var suspects = new List<string>
            {
                "Miss Scarlet", "Professor Plum", "Mrs. Peacock", "Mr. Green", "Colonel Mustard", "Mrs. White"
            };

            var adjectives = new List<string>
            {
                "tall", "evil", "bemustached", "a total slob"
            };

            var root = QuestionTreeBuilder.BuildNewTree(QuestionType.Is);
            foreach (var suspect in suspects)
            {
                root.AddSubject(suspect);
            }
            foreach (var adjective in adjectives)
            {
                root.AddAdjective(adjective);
            }

            root.QuestionType = QuestionType.Is;
            return root;
        }

        private static QuestionNode BuildDidTree()
        {
            var suspects = new List<string>
            {
                "Miss Scarlet", "Professor Plum", "Mrs. Peacock", "Mr. Green", "Colonel Mustard", "Mrs. White"
            };

            var victims = new List<string>
            {
                "the cat", "Mr. Mulberry", "Roger Rabbit", "Mr. Boddy"
            };

            var root = QuestionTreeBuilder.BuildNewTree(QuestionType.Did);

            foreach (var suspect in suspects)
            {
                root.AddSubject(suspect);
            }

            root.AddVerb("stab", "stabbed");
            root.AddVerb("shoot", "shot");
            root.AddVerb("strangle", "strangled");
            root.AddVerb("maul", "mauled");
            root.AddVerb("poison", "poisoned");
            root.AddVerb("throw a piano on", "threw a piano on");

            foreach (var victim in victims)
            {
                root.AddObject(victim);
            }

            root.QuestionType = QuestionType.Did;
            return root;
        }
    }
}
