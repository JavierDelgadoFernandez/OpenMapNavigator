using OpenMapNavigator.Elements.Attrbitutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Map
{
    public class Node : MapElementBase
    {
        public Coordinate Coordinate { get; private set; }

        public Node(long id, ICollection<IAttribute> attributes, Coordinate coordinate)
            : base(id, attributes)
        {
            this.Coordinate = coordinate;
        }
    }
}
