using Bdi.Objects.Attribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Objects
{
    public abstract class Noun
    {
        public string Name { get; set; }

        public AttributeSet Attributes { get; set; }

        public Noun(string name)
        {
            Name = name;
        }
    }
}
