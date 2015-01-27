using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenMapNavigator.Elements.Map;
using OpenMapNavigator.Engine;
using OpenMapNavigator.GraphicsEngine.Graphics;
using System;
using System.Collections.Generic;
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

        public void GraphicLoader(out VertexBuffer vertexbuffer, out IndexBuffer indexbuffer)
        {
            /*VertexPositionColor[] vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(-1,-1, 0), Color.Red);
            
            vertexbuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 4, BufferUsage.WriteOnly);
            vertexbuffer.SetData<VertexPositionColor>(vertices);

            short[] indices = new short[6];
            indices[0] = 0; indices[1] = 1; indices[2] = 3;
            indices[3] = 1; indices[4] = 2; indices[5] = 3;

            indexbuffer = new IndexBuffer(GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexbuffer.SetData(indices);*/

            var vertices = PartialGraphicMaps.First().Vertices;

            vertexbuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexbuffer.SetData(vertices);

            var indices = PartialGraphicMaps.First().Indices;

            indexbuffer = new IndexBuffer(GraphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            indexbuffer.SetData(indices);
        }
    }
}
