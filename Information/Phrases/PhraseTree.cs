using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases
{
    public class PhraseTree
    {
        public PhraseNode Root { get; set; }

        public List<PhraseNode> SelectedNodes { get; set; }

        public PhraseNode CurrentNode
        {
            get
            {
                if (SelectedNodes.Any())
                {
                    return SelectedNodes.Last();
                }
                else
                {
                    return Root;
                }
            }
        }

        public List<PhraseNode> NextNodes
        {
            get
            {
                return CurrentNode.Next;
            }
        }

        private PhraseTree()
        {
            SelectedNodes = new List<PhraseNode>();
        }

        public PhraseTree(PhraseNode root) : this()
        {
            Root = root;
        }

        public void Select(PhraseNode node)
        {
            SelectedNodes.Add(node);
        }

        public void GoBack()
        {
            if (CurrentNode != Root)
            {
                var lastIndex = SelectedNodes.Count - 1;
                SelectedNodes.RemoveAt(lastIndex);
            }
        }

        public void ResetSelection()
        {
            SelectedNodes.Clear();
        }

        protected Phrase GetSelectedPhrase()
        {
            var phrase = new Phrase();

            foreach (var node in SelectedNodes)
            {
                switch (node.WordType)
                {
                    case WordType.Subject:
                        phrase.Subject = node.BaseWord;
                        break;
                    case WordType.Verb:
                        phrase.Verb = node.BaseWord;
                        break;
                    case WordType.Object:
                        phrase.Object = node.BaseWord;
                        break;
                    case WordType.Adjective:
                        phrase.Adjective = node.BaseWord;
                        break;
                    default:
                        break;
                }
            }

            return phrase;
        }
    }
}
