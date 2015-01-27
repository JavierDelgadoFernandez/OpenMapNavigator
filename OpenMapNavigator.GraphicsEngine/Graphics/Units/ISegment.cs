using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.GraphicsEngine.Graphics.Units
{
    public interface ISegment
    {
        uint Priority { get; }

        IList<IGraphic> Graphics { get; } 
    }
}
