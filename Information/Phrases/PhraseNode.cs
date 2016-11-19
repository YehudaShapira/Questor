using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases
{
    public abstract class PhraseNode
    {
        internal Tense Tense { get; set; }
        internal string BaseWord { get; set; }

        /// <summary>
        /// Word being held in this node
        /// </summary>
        public string Word
        {
            get
            {
                if (WordType == WordType.Verb)
                {
                    if (Tense == Tense.Base)
                    {
                        return BaseWord;
                    }
                    else if (Tense == Tense.Past)
                    {
                        return VerbTenseDictionary.Past[BaseWord];
                    }
                    else if (Tense  == Tense.Progressive)
                    {
                        return BaseWord;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    return BaseWord;
                }
            }
        }

        /// <summary>
        /// Word type of this specific node
        /// </summary>
        public WordType WordType { get; set; }

        /// <summary>
        /// All possible next words
        /// </summary>
        public List<PhraseNode> Next { get; set; }

        /// <summary>
        /// Placeholder nodes are not meant to be displayed or used
        /// </summary>
        public bool IsPlaceholder { get; set; }

        private PhraseNode()
        {
            Next = new List<PhraseNode>();
            IsPlaceholder = false;
        }

        public PhraseNode(string word, WordType wordType, Tense tense)
            : this()
        {
            BaseWord = word;
            WordType = wordType;
            Tense = tense;
        }

        public void AddNode(PhraseNode node)
        {
            var nodeWithSameWord = Next.FirstOrDefault(n => n.BaseWord == node.BaseWord);
            if (nodeWithSameWord == null)
            {
                Next.Add(node);
            }
            else
            {
                MergeNodes(nodeWithSameWord, node);
            }
        }

        private void MergeNodes(PhraseNode existingNode, PhraseNode newNode)
        {
            foreach (var newNext in newNode.Next)
            {
                if (!newNext.IsPlaceholder)
                {
                    existingNode.AddNode(newNext);
                }
            }
        }

        public void AddSubject(string subject)
        {
            foreach (var node in Next)
            {
                node.AddSubject(subject);
            }

            var subjectChild = Next.FirstOrDefault(n => n.WordType == WordType.Subject);
            if (subjectChild != null)
            {
                var duplicate = subjectChild.Duplicate();
                duplicate.BaseWord = subject;
                duplicate.IsPlaceholder = false;
                AddNode(duplicate);
            }
        }

        public void AddVerb(string baseTense, string pastTense)
        {
            VerbTenseDictionary.Register(baseTense, pastTense);
            AddVerb(baseTense);
        }

        protected void AddVerb(string baseTense)
        {
            foreach (var node in Next)
            {
                node.AddVerb(baseTense);
            }

            var verbChild = Next.FirstOrDefault(n => n.WordType == WordType.Verb);
            if (verbChild != null)
            {
                var duplicate = verbChild.Duplicate();
                duplicate.BaseWord = baseTense;
                duplicate.IsPlaceholder = false;
                AddNode(duplicate);
            }
        }

        public void AddObject(string obj)
        {
            foreach (var node in Next)
            {
                node.AddObject(obj);
            }

            var objectChild = Next.FirstOrDefault(n => n.WordType == WordType.Object);
            if (objectChild != null)
            {
                var duplicate = objectChild.Duplicate();
                duplicate.BaseWord = obj;
                duplicate.IsPlaceholder = false;
                AddNode(duplicate);
            }
        }

        public void AddAdjective(string adjective)
        {
            foreach (var node in Next)
            {
                node.AddAdjective(adjective);
            }

            var adjectiveChild = Next.FirstOrDefault(n => n.WordType == WordType.Adjective);
            if (adjectiveChild != null)
            {
                var duplicate = adjectiveChild.Duplicate();
                duplicate.BaseWord = adjective;
                duplicate.IsPlaceholder = false;
                AddNode(duplicate);
            }
        }

        public PhraseNode Duplicate()
        {
            var duplicate = DuplicateBasic();
            duplicate.IsPlaceholder = this.IsPlaceholder;

            foreach (var node in Next)
            {
                var duplicateNode = node.Duplicate();
                duplicate.Next.Add(duplicateNode);
            }

            return duplicate;
        }

        protected abstract PhraseNode DuplicateBasic();
    }
}
