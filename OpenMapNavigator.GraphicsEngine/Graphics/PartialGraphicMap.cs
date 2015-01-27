using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements;
using OpenMapNavigator.Elements.Map;
using OpenMapNavigator.GraphicsEngine.Graphics.Elements;
using OpenMapNavigator.GraphicsEngine.Graphics.Units;
using OpenMapNavigator.GraphicsEngine.Graphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics
{
    public class PartialGraphicMap : IElement
    {
        public Coordinate TopLeft { get; private set; }
        public Coordinate BottomRight { get; private set; }

        private Map map;

        private IList<IElement> _elements;

        public PartialGraphicMap(Map map, Coordinate topleft, Coordinate bottomright)
        {
            this.TopLeft = topleft;
            this.BottomRight = bottomright;
            this.map = map;

            LoadGraphics();
        }

        private void LoadGraphics()
        {
            _elements = new List<IElement>();
            foreach (Way way in map.Ways.Values)
                _elements.Add(new WayBinding(way));
        }

        public IList<ISegment> Segments
        {
            get 
            {
                List<ISegment> result = new List<ISegment>();

                foreach (var item in _elements)
                    result.AddRange(item.Segments);

                return result;
            }
        }
    }
}
