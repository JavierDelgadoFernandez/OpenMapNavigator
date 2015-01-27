using OpenMapNavigator.Elements.Attrbitutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Map
{
    public class Relation : MapElementBase
    {
        public IList<MapElementBase> Members { get; private set; }

        public Relation (long id, ICollection<IAttribute> attributes, IList<MapElementBase> members)
            : base(id, attributes)
        {
            this.Members = members;
        }
    }
}
