using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Statements
{
    public enum StatementType
    {
        /// <summary>
        /// [Subject] [verbed] [object].
        /// </summary>
        Did,

        /// <summary>
        /// [Subject] did not [verb] [object].
        /// </summary>
        DidNot,

        /// <summary>
        /// [Subject] is [adjective].
        /// </summary>
        Is,

        /// <summary>
        /// [Subject] is not [adjective].
        /// </summary>
        IsNot
    }
}
