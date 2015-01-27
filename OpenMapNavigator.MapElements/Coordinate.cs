using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMapNavigator.Elements
{
    public class Coordinate
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public Coordinate(float latitude, float longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
