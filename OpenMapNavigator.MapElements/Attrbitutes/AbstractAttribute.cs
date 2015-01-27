using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Attrbitutes
{
    public class AbstractAttribute : IAttribute
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public AbstractAttribute(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
