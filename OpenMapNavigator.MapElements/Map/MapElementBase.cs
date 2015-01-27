using OpenMapNavigator.Elements.Attrbitutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements.Map
{
    public abstract class MapElementBase
    {
        public long ID { get; private set; }

        public ICollection<IAttribute> Attributes { get; private set; }

        public MapElementBase(long id, ICollection<IAttribute> attributes)
        {
            this.ID = id;
            this.Attributes = attributes;
        }
    }
}
