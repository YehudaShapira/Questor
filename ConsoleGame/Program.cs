using Questor.Inquiry.Phrases.Questioning;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Questor.Inquiry.Dummy;
using Questor.Inquiry.Data;
using Questor.Inquiry.Phrases.Statements;
using Questor.Characters;

namespace Questor.ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Qa();
            
            Console.ReadLine();
        }

        private static void Qa()
        {
            var chuck = new Character("Chuck");
            var sentiment = new Feelings();
            sentiment.Faith = 100;
            chuck.CharacterSentiment["Player"] = sentiment;

            while (true)
            {
                Console.Clear();
                Console.Write("Press Q to ask question, A to make statement:");
                var c = Console.ReadKey();
                while (c.Key != ConsoleKey.Q && c.Key != ConsoleKey.A)
                {
                    Console.Clear();
                    Console.Write("Press Q to ask question, A to make statement:");
                    c = Console.ReadKey();
                }

                Console.Clear();
                if (c.Key == ConsoleKey.Q)
                {
                    DoSuspects(chuck);
                }
                else if (c.Key == ConsoleKey.A)
                {
                    DoStatement(chuck);
                }
            }
        }

        private static void DoStatement(Character character)
        {
            Console.WriteLine("Make a statement:");

            var tree = DummyStatementTreeBuilder.BuildTree();
            var chooser = new StatementChooser(tree, 0, 1);

            var statement = chooser.GetPlayerChoice();

            var response = character.Listen(statement, "Player");

            Console.WriteLine();
            Console.WriteLine(character.Name + "'s reponse is " + response);
            Console.ReadKey();
            switch (response)
            {
                case Expression.Surprise:
                    break;
                case Expression.Nod:
                    break;
                case Expression.Frown:
                    break;
                case Expression.Skeptic:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void DoSuspects(Character character)
        {
            Console.WriteLine("Ask a question:");

            var tree = DummyQuestionTreeBuilder.BuildTree();

            var chooser = new QuestionChooser(tree, 0, 1);
            var question = chooser.GetPlayerChoice();

            var answer = character.Answer(question);
            Console.WriteLine();
            Console.WriteLine("\"" + answer.ToString() + "\"");

            Console.ReadKey();
        }
    }
}
