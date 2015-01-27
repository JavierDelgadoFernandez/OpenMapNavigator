using Microsoft.Xna.Framework;
using OpenMapNavigator.GraphicsEngine.Graphics.Util;
using System.Collections.Generic;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    class SegmentJunction : ISegment
    {
        public PolarGraphicCoordinate Coord1 { get; private set; }
        public PolarGraphicCoordinate Coord2 { get; private set; }

        public Color Color { get; private set; }

        public double Width { get; private set; }

        private Square _square;

        public uint Priority { get; private set; }

        public SegmentJunction(PolarGraphicCoordinate coord1, PolarGraphicCoordinate coord2, Color color, double width, uint priority)
        {
            this.Coord1 = coord1;
            this.Coord2 = coord2;
            this.Color = color;
            this.Width = width;
            this.Priority = priority;

            LoadGraphics();
        }

        private void LoadGraphics()
        {
            var bottomleft = new PolarGraphicCoordinate(Coord1.Target, Width, Coord1.PerpendicularNegative().AngleDegree);
            var topleft = new PolarGraphicCoordinate(Coord1.Target, Width, Coord1.PerpendicularPositive().AngleDegree);
            var topright = new PolarGraphicCoordinate(Coord2.Origin, Width, Coord2.PerpendicularPositive().AngleDegree);
            var bottomright = new PolarGraphicCoordinate(Coord2.Origin, Width, Coord2.PerpendicularNegative().AngleDegree);

            _square = new Square(bottomleft.Target, topleft.Target, topright.Target, bottomright.Target, Color);
        }

        public IList<IGraphic> Graphics
        {
            get
            {
                var result = new List<IGraphic>();
                result.Add(_square);
                return result;
            }
        }
    }
}
