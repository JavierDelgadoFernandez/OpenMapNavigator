using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.GraphicsEngine.Graphics.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    class Segment : ISegment
    {
        public GraphicCoordinate Origin { get; private set; }
        public GraphicCoordinate Target { get; private set; }

        public Color Color { get; private set; }

        public double Width { get; private set; }
        public uint Priority { get; private set; }

        private Square _square;

        public Segment(GraphicCoordinate origin, GraphicCoordinate target, Color color, double width, uint priority)
        {
            this.Origin = origin;
            this.Target = target;
            this.Color = color;
            this.Width = width;
            this.Priority = priority;
            LoadGraphics();
        }

        public Segment(PolarGraphicCoordinate polargraphiccoordinate, Color color, double width, uint priority)
        {
            this.Origin = polargraphiccoordinate.Origin;
            this.Target = polargraphiccoordinate.Target;
            this.Color = color;
            this.Width = width;
            this.Priority = priority;
            LoadGraphics();
        }

        private void LoadGraphics()
        {
            PolarGraphicCoordinate pgc = new PolarGraphicCoordinate(Origin, Target);

            var topleft = new PolarGraphicCoordinate(Origin, Width, pgc.PerpendicularPositive().AngleDegree);
            var bottomleft = new PolarGraphicCoordinate(Origin, Width, pgc.PerpendicularNegative().AngleDegree);
            var topright = new PolarGraphicCoordinate(Target, Width, pgc.PerpendicularPositive().AngleDegree);
            var bottomright = new PolarGraphicCoordinate(Target, Width, pgc.PerpendicularNegative().AngleDegree);

            var y = topleft.Target.Y - bottomleft.Target.Y;
            var y0 = topleft.Target.Y - topright.Target.Y;
            var y2 = topright.Target.Y - bottomleft.Target.Y;
            var y3 = topright.Target.Y - bottomright.Target.Y;
            var y4 = bottomleft.Target.Y - bottomright.Target.Y;

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
