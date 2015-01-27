using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements.Map;
using OpenMapNavigator.GraphicsEngine.Graphics.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Elements
{
    public class WayBinding : IElement
    {
        private Way _way;
        private WayGraphic _waygraphic;

        public WayBinding(Way way)
        {
            this._way = way;
            LoadGraphics();
        }

        private void LoadGraphics()
        {
            var coordinates = _way.Nodes.Select(n => GraphicCoordinate.FromCoordinate(n.Coordinate)).ToArray();

            _waygraphic = new WayGraphic(coordinates, GetVertexConfiguration());
        }

        public IList<ISegment> Segments
        {
            get 
            {
                return _waygraphic.Segments;
            }
        }

        private IList<VertexConfiguration> GetVertexConfiguration()
        {
            if (_way.Attributes.Any(a => a.Key == "highway"))
                return GetVertexConfigurationHighway();
            return new List<VertexConfiguration>();
        }

        private IList<VertexConfiguration> GetVertexConfigurationHighway()
        {
            IList<VertexConfiguration> result = new List<VertexConfiguration>();
            switch (_way.Attributes.Single(a => a.Key == "highway").Value)
            {
                case "motorway": case "motorway_link":
                    result.Add(new VertexConfiguration(Color.Black, 14, 2));
                    result.Add(new VertexConfiguration(Color.Orange, 12, 8));
                    break;
                case "trunk": case "trunk_link":
                    result.Add(new VertexConfiguration(Color.Black, 12, 2));
                    result.Add(new VertexConfiguration(Color.Red, 10, 7));
                    break;
                case "primary": case "primary_link":
                    result.Add(new VertexConfiguration(Color.Black, 10, 2));
                    result.Add(new VertexConfiguration(Color.Green, 8, 6));
                    break;
                case "secondary": case "secondary_link":
                    result.Add(new VertexConfiguration(Color.Black, 10, 2));
                    result.Add(new VertexConfiguration(Color.Yellow, 8, 5));
                    break;
                case "tertiary": case "tertiary_link":
                    result.Add(new VertexConfiguration(Color.Black, 8, 2));
                    result.Add(new VertexConfiguration(Color.Crimson, 6, 4));
                    break;
                case "residential":
                    result.Add(new VertexConfiguration(Color.Black, 8, 2));
                    result.Add(new VertexConfiguration(Color.LightYellow, 6, 3));
                    break;
                default:
                    result.Add(new VertexConfiguration(Color.Black, 1, 1));
                    break;
            }
            return result;
        }
    }
}
