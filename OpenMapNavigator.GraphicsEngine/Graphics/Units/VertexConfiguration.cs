using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    public class VertexConfiguration
    {
        public Color Color { get; private set; }
        public double Width { get; private set; }
        public uint Priority { get; private set; }

        public VertexConfiguration(Color color, double width, uint priority)
        {
            if (priority > 9 || priority < 0)
                throw new ArgumentException("Priority must be between [0 - 10)");
            this.Color = color;
            this.Width = width;
            this.Priority = priority;
        }

        internal IList<ISegment> GetSegments(IList<PolarGraphicCoordinate> coordinates)
        {
            IList<ISegment> result = new List<ISegment>();
            result.Add(new Segment(coordinates[0], Color, Width, Priority));
            for (int i = 1; i < coordinates.Count; i++)
            {
                result.Add(new SegmentJunction(coordinates[i - 1], coordinates[i], Color, Width, Priority));
                result.Add(new Segment(coordinates[i], Color, Width, Priority));
            }
            return result;
        }
    }
}
