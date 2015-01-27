using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Attrbitutes
{
    public interface IAttribute
    {
        string Key { get; }

        string Value { get; }
    }
}
