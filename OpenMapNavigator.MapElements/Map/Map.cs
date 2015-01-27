using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Map
{
    public class Map
    {

        public IDictionary<long, Node> Nodes { get; private set; }

        public IDictionary<long, Way> Ways { get; private set; }

        public IDictionary<long, Relation> Relations { get; private set; }

        public Map()
        {
            Nodes = new Dictionary<long, Node>();
            Ways = new Dictionary<long, Way>();
            Relations = new Dictionary<long, Relation>();
        }

        public void Add(Node node)
        {
            Nodes.Add(node.ID, node);
        }

        public void Add(Way way)
        {
            Ways.Add(way.ID, way);
        }

        public void Add(Relation relation)
        {
            Relations.Add(relation.ID, relation);
        }
    }
}
