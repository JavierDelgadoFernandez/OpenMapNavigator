using OpenMapNavigator.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Util
{
    public class PolarCoordinate
    {
        public Coordinate Origin { get; private set; }
        public double Distance { get; private set; }

        public double AngleDegree { get; private set; }

        public double Phase 
        { 
            get
            {
                return AngleDegree * 2 * Math.PI / 360;
            }
        }


        public Coordinate Target
        {
            get
            {
                return new Coordinate(
                    (float)(Origin.Latitude + Distance * Math.Sin(Phase)), 
                    (float)(Origin.Longitude + Distance * Math.Cos(Phase)));
            }
        }

        public PolarCoordinate(Coordinate origin, double distance, double angledegrees)
        {
            this.Origin = origin;
            this.Distance = distance;
            this.AngleDegree = angledegrees;
        }

        public PolarCoordinate(Coordinate coordinate0, Coordinate coordinate1)
        {
            double y = coordinate1.Latitude - coordinate0.Latitude;
            double x = coordinate1.Longitude - coordinate0.Longitude;

            this.Origin = coordinate0;
            Distance = Math.Sqrt((x * x) + (y * y));
            AngleDegree = Math.Atan2(y, x) * 180/Math.PI + 180;
        }

        public PolarCoordinate PerpendicularPositive()
        {
            //return new PolarCoordinate(Origin, Distance, (Phase + Math.PI / 2) / (2 * Math.PI));
            return new PolarCoordinate(Origin, Distance, (AngleDegree + 90) % 360);
        }

        public PolarCoordinate PerpendicularNegative()
        {
            return new PolarCoordinate(Origin, Distance, Math.Abs((AngleDegree - 90) % 360));
        }
    }
}
