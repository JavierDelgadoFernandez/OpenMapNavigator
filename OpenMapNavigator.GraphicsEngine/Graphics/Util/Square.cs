using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements;
using OpenMapNavigator.GraphicsEngine.Graphics.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Util
{
    public class Square : IGraphic
    {
        public Vector3 A
        {
            get
            {
                return _t1.A;
            }
        }
        public Vector3 B
        {
            get
            {
                return _t1.B;
            }
        }
        public Vector3 C
        {
            get
            {
                return _t1.C;
            }
        }
        public Vector3 D
        {
            get
            {
                return _t2.C;
            }
        }

        public Color Color
        {
            get
            {
                return _t1.Color;
            }
        }

        private Triangle _t1;
        private Triangle _t2;

        public Square(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
        {
            _t1 = new Triangle(a, b, c, color);
            _t2 = new Triangle(c, d, a, color);
        }

        public Square(Coordinate a, Coordinate b, Coordinate c, Coordinate d, Color color)
        {
            _t1 = new Triangle(a, b, c, color);
            _t2 = new Triangle(c, d, a, color);
        }

        public Square(GraphicCoordinate a, GraphicCoordinate b, GraphicCoordinate c, GraphicCoordinate d, Color color)
        {
            _t1 = new Triangle(a, b, c, color);
            _t2 = new Triangle(c, d, a, color);
        }

        private VertexPositionColor[] _vertices;

        public VertexPositionColor[] Vertices
        {
            get
            {
                if (_vertices == null)
                    LoadVertices();
                return _vertices;
            }
        }

        private void LoadVertices()
        {
                var vertices1 = _t1.GetVertices();
                var vertices2 = _t2.GetVertices();

#if DEBUG
                Debug.Assert(vertices1.Length == vertices2.Length);
#endif

                VertexPositionColor[] result = new VertexPositionColor[vertices1.Length * 2];

                for (int i = 0; i < vertices1.Length; i++)
                {
                    result[i] = vertices1[i];
                    result[i + vertices1.Length] = vertices2[i];
                }

                _vertices = result;
        }
    }
}
