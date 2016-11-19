using Questor.Inquiry.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.ConsoleGame
{
    public abstract class PhraseChooser
    {
        protected int left;
        protected int top;
        protected Stack<int> wordEnds;
        protected PhraseTree tree;

        protected abstract string PhraseEnd
        {
            get;
        }

        public PhraseChooser(PhraseTree tree, int left, int top)
        {
            this.tree = tree;
            this.left = left;
            this.top = top;
            wordEnds = new Stack<int>();
        }

        protected void ShowRoot()
        {
            Console.SetCursorPosition(left, top);
            var first = tree.Root.Word + " ";
            Console.Write(first);
            wordEnds.Push(first.Length);
        }

        protected void GetNextSelection()
        {
            var choice = 0;
            var usuableTotal = tree.NextNodes.Where(n => !n.IsPlaceholder).Count();
            var dummyCount = tree.NextNodes.Count - usuableTotal;

            var stillChoosing = true;

            while (stillChoosing)
            {
                ShowChoices(choice, usuableTotal, dummyCount);

                var c = Console.ReadKey();
                if (c.Key == ConsoleKey.UpArrow)
                {
                    choice += usuableTotal;
                    choice--;
                    choice %= usuableTotal;
                }
                else if (c.Key == ConsoleKey.DownArrow)
                {
                    choice++;
                    choice %= usuableTotal;
                }
                else if (c.Key == ConsoleKey.RightArrow)
                {
                    HidePreviousChoices(usuableTotal, dummyCount);
                    SelectHighlighted(choice, dummyCount);
                    WriteSelectedChoice();

                    stillChoosing = false;
                }
                else if (c.Key == ConsoleKey.LeftArrow && tree.CurrentNode != tree.Root)
                {
                    HidePreviousChoices(usuableTotal, dummyCount);
                    ClearSelectedWord();
                    TakeBackSelection();

                    stillChoosing = false;
                }
            }
        }

        protected void ShowChoices(int choice, int total, int dummyCount)
        {
            for (var i = 0; i < total; i++)
            {
                if (i == choice)
                {
                    var temp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = temp;
                }

                Console.SetCursorPosition(wordEnds.Peek(), top + i);
                Console.Write(tree.NextNodes[i + dummyCount].Word);
                if (tree.NextNodes[i + dummyCount].Next.Any())
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(PhraseEnd);
                }

                Console.ResetColor();
            }
        }

        protected void SelectHighlighted(int choice, int dummyCount)
        {
            var highlighted = tree.NextNodes[choice + dummyCount];
            tree.Select(highlighted);
        }

        protected void HidePreviousChoices(int total, int dummyCount)
        {
            for (var i = 0; i < total; i++)
            {
                Console.SetCursorPosition(wordEnds.Peek(), top + i);
                for (var j = 0; j <= tree.NextNodes[i + dummyCount].Word.Length; j++)
                {
                    Console.Write(" ");
                }
            }
        }

        protected void WriteSelectedChoice()
        {
            Console.SetCursorPosition(wordEnds.Peek(), top);
            Console.Write(tree.CurrentNode.Word);
            if (tree.NextNodes.Any())
            {
                Console.Write(" ");
            }
            else
            {
                Console.Write(PhraseEnd);
            }

            wordEnds.Push(Console.CursorLeft);
        }

        protected void ClearSelectedWord()
        {
            var currentLeft = wordEnds.Pop();
            var previousLeft = wordEnds.Peek();
            Console.SetCursorPosition(previousLeft, top);
            while (Console.CursorLeft < currentLeft)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(previousLeft, top);
        }

        protected void TakeBackSelection()
        {
            tree.GoBack();
        }
    }
}
