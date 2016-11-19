using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bdi.Concepts.Statements
{
    //[XmlRoot(Namespace = Constants.Namespace)]
    [XmlInclude(typeof(CompoundStatement))]
    [XmlInclude(typeof(DynamicStatement))]
    [XmlInclude(typeof(StaticStatement))]
    public abstract class Statement
    {
        public abstract bool IsConsistent(Statement other);

        public static readonly Statement Empty = new EmptyStatement();
    }
}
