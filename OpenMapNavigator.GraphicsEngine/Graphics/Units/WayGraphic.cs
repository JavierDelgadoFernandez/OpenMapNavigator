using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    public class WayGraphic : IElement
    {
        public IList<ISegment> Segments { get; private set; }
        public IList<VertexConfiguration> Configurations { get; private set; }

        public WayGraphic(IList<GraphicCoordinate> coordinates, IList<VertexConfiguration> configurations)
        {
            this.Configurations = configurations;

            var pgcs = new List<PolarGraphicCoordinate>();
            for (int i = 0; i < coordinates.Count - 1; i++)
                pgcs.Add(new PolarGraphicCoordinate(coordinates[i], coordinates[i + 1]));
            LoadGraphics(pgcs);
        }
        public WayGraphic(IList<PolarGraphicCoordinate> coordinates, IList<VertexConfiguration> configurations)
        {
            this.Configurations = configurations;
            LoadGraphics(coordinates);
        }
        private void LoadGraphics(IList<PolarGraphicCoordinate> coordinates)
        {
            this.Segments = new List<ISegment>();
            foreach (VertexConfiguration vc in Configurations)
            {
                IList<ISegment> segments = vc.GetSegments(coordinates);
                foreach (ISegment item in segments)
                    this.Segments.Add(item);
            }
        }
    }
}
