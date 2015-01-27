using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements;
using OpenMapNavigator.GraphicsEngine.Graphics.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Util
{
    public class Triangle
    {
        public Vector3 A { get; private set; }
        public Vector3 B { get; private set; }
        public Vector3 C { get; private set; }
        public Color Color { get; private set; }

        public Triangle(Coordinate a, Coordinate b, Coordinate c, Color color)
        {
            this.A = new Vector3(a.Longitude, a.Latitude, 0);
            this.B = new Vector3(b.Longitude, b.Latitude, 0);
            this.C = new Vector3(c.Longitude, c.Latitude, 0);
            this.Color = color;
        }

        public Triangle(Vector3 a, Vector3 b, Vector3 c, Color color)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.Color = color;
        }

        public Triangle(GraphicCoordinate a, GraphicCoordinate b, GraphicCoordinate c, Color color)
        {
            this.A = a.ToVector3();
            this.B = b.ToVector3();
            this.C = c.ToVector3();
            this.Color = color;
        }

        public VertexPositionColor[] GetVertices()
        {
            VertexPositionColor[] result = new VertexPositionColor[3];
            
            result[0] = new VertexPositionColor(A, Color);
            result[1] = new VertexPositionColor(B, Color);
            result[2] = new VertexPositionColor(C, Color);

            return result;
        }
    }
}
