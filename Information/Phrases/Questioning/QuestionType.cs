using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questor.Inquiry.Phrases.Questioning
{
    public enum QuestionType
    {
        /// <summary>
        /// Did [subject] [verb] [object]?
        /// </summary>
        Did,

        /// <summary>
        /// Does [subject] [verb] ([object])?
        /// </summary>
        Does,

        /// <summary>
        /// Do you [verb] ([object])?
        /// </summary>
        DoYou,

        /// <summary>
        /// Is [subject] [adjective]?
        /// </summary>
        Is,

        /// <summary>
        /// Are you [adjective]?
        /// </summary>
        AreYou,

        /// <summary>
        /// When did [subject] [verb] [object]?
        /// </summary>
        WhenDid,

        /// <summary>
        /// Where did [subject] [verb] [object]?
        /// </summary>
        WhereDid,

        /// <summary>
        /// Where is [subject]?
        /// </summary>
        WhereIs,

        /// <summary>
        /// Who [verb] [object]?
        /// </summary>
        WhoDid,

        /// <summary>
        /// Who [verb] ([object])?
        /// </summary>
        WhoDoes,

        /// <summary>
        /// Who is [subject]?
        /// </summary>
        WhoIs,

        /// <summary>
        /// Why did [subject] [verb] [object]?
        /// </summary>
        WhyDid
    }
}
