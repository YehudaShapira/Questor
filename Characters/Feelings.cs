using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Characters
{
    /// <summary>
    /// The feelings one character has towards another
    /// </summary>
    public class Feelings
    {
        /// <summary>
        /// How much character A believes character B
        /// </summary>
        public int Faith { get; set; }

        /// <summary>
        /// How much character A is afraid of character B
        /// </summary>
        public int Fear { get; set; }

        /// <summary>
        /// How fond character A is of character B
        /// </summary>
        public int Fondness { get; set; }
    }
}
