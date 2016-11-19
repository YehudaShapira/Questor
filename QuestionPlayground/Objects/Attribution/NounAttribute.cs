using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Objects.Attribution
{
    public class NounAttribute
    {
        public string Key { get; set; }

        public string Value { get; set; }

        private NounAttribute()
        {

        }

        public NounAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
