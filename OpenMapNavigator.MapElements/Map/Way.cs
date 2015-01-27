using OpenMapNavigator.Elements.Attrbitutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Map
{
    public class Way : MapElementBase
    {
        public IList<Node> Nodes { get; private set; }

        public Way(long id, ICollection<IAttribute> attributes, IList<Node> nodes)
            : base(id, attributes)
        {
            this.Nodes = nodes;
        }
    }
}
