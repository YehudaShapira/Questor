using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases
{
    public static class VerbTenseDictionary
    {
        public static Dictionary<string, string> Base { get; private set; }
        public static Dictionary<string, string> Past { get; private set; }

        static VerbTenseDictionary()
        {
            Base = new Dictionary<string, string>();
            Past = new Dictionary<string, string>();
        }

        /// <summary>
        /// Registers tenses in dictionary.
        /// Can be called multiple times, with no ill affect.
        /// </summary>
        /// <param name="baseTense"></param>
        /// <param name="pastTense"></param>
        public static void Register(string baseTense, string pastTense)
        {
            Base[pastTense] = baseTense;
            Past[baseTense] = pastTense;
        }
    }
}
