using Microsoft.Xna.Framework;
using OpenMapNavigator.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    public class GraphicCoordinate
    {
        private const int SCALE_FACTOR = 100000;

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; set; }

        public GraphicCoordinate(float x, float y, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public static GraphicCoordinate FromCoordinate(Coordinate coordinate)
        {
            return new GraphicCoordinate(
                coordinate.Longitude * SCALE_FACTOR,
                coordinate.Latitude * SCALE_FACTOR);
        }
    }
}
