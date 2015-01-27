using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements.Map;
using OpenMapNavigator.Engine;
using OpenMapNavigator.GraphicsEngine.Graphics;
using OpenMapNavigator.GraphicsEngine.Graphics.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine
{
    public class GraphicNavigator
    {
        public Navigator Navigator { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

        public IList<PartialGraphicMap> PartialGraphicMaps { get; private set; }

        public GraphicNavigator(GraphicsDevice graphicsdevice)
        {
            this.Navigator = new Navigator();

            this.GraphicsDevice = graphicsdevice;

            PartialGraphicMaps = new List<PartialGraphicMap>();
            PartialGraphicMaps.Add(new PartialGraphicMap(Navigator.Map, null, null));
        }

        VertexBuffer _vertexbuffer = null;

        public VertexBuffer GetVertex()
        {
#if DEBUG
            DateTime t0 = DateTime.Now;
#endif
            if (_vertexbuffer == null)
                _vertexbuffer = GetVertexBuffer();

#if DEBUG
            Debug.WriteLine("The renderer has spend {0}", DateTime.Now - t0);
#endif

            return  _vertexbuffer;
        }

        private VertexBuffer GetVertexBuffer()
        {
            VertexBuffer vertexbuffer;
            List<ISegment> segments = new List<ISegment>();
            foreach (var pgm in PartialGraphicMaps)
                segments.AddRange(pgm.Segments);

            var orderedsegments = segments.OrderBy(s => s.Priority);

            var vertices = new List<VertexPositionColor>();

            foreach (var segment in orderedsegments)
                foreach (var graphic in segment.Graphics)
                    vertices.AddRange(graphic.Vertices);

            vertexbuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Count, BufferUsage.WriteOnly);
            vertexbuffer.SetData(vertices.ToArray());
            return vertexbuffer;
        }
    }
}
