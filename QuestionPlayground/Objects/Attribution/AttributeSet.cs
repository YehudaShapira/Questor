using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Objects.Attribution
{
    public class AttributeSet
    {
        private Dictionary<string, string> attributes;

        public void Add(NounAttribute attribute)
        {
            attributes.Add(attribute.Key, attribute.Value);
        }

        public bool ContainsAttribute(NounAttribute attribute)
        {
            return attributes.ContainsKey(attribute.Key);
        }

        public ICollection<string> Keys
        {
            get { return attributes.Keys; }
        }

        public bool Remove(NounAttribute attribute)
        {
            return attributes.Remove(attribute.Key);
        }

        public string this[string key]
        {
            get
            {
                return attributes[key];
            }
            set
            {
                attributes[key] = value;
            }
        }

        public void Clear()
        {
            attributes.Clear();
        }

        public int Count
        {
            get { return attributes.Count; }
        }

        public AttributeSet()
        {
            attributes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
