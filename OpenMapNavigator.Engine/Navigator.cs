using OpenMapNavigator.Elements.Map;
using OpenMapNavigator.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Engine
{
    public class Navigator
    {
        public Map Map { get; private set; }

        public Navigator()
        {
            Stream stream = Assembly.Load(new AssemblyName("OpenMapNavigator.Engine")).GetManifestResourceStream("OpenMapNavigator.Engine.map.osm");
            this.Map = OSMXML.Load(stream);
        }
    }
}
