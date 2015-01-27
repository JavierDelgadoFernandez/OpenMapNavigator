using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    public class PolarGraphicCoordinate
    {
        public GraphicCoordinate Origin { get; private set; }
        public double Distance { get; private set; }
        public double AngleDegree { get; private set; }
        public double Phase
        {
            get
            {
                return (AngleDegree - 180) * Math.PI / 180;
            }
        }
        public GraphicCoordinate Target
        {
            get
            {
                return new GraphicCoordinate(
                    (float)(Origin.X + Distance * Math.Cos(Phase)),
                    (float)(Origin.Y + Distance * Math.Sin(Phase)),
                    Origin.Z);
            }
        }

        public PolarGraphicCoordinate(GraphicCoordinate origin, double distance, double angledegree)
        {
            this.Origin = origin;
            this.Distance = distance;
            this.AngleDegree = angledegree;
        }

        public PolarGraphicCoordinate(GraphicCoordinate origin, GraphicCoordinate target)
        {
            double x = target.X - origin.X;
            double y = target.Y - origin.Y;

            this.Origin = origin;
            Distance = Math.Sqrt((x * x) + (y * y));
            AngleDegree = Math.Atan2(y, x) * 180 / Math.PI + 180;
        }

        public PolarGraphicCoordinate PerpendicularPositive()
        {
            return new PolarGraphicCoordinate(Origin, Distance, (AngleDegree + 90) % 360);
        }

        public PolarGraphicCoordinate PerpendicularNegative()
        {
            var angle = (AngleDegree - 90);
            if (angle < 0)
                angle = 360 + angle;
            return new PolarGraphicCoordinate(Origin, Distance, angle);
        }
    }
}
