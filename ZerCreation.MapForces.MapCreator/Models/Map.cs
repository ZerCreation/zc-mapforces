using System;
using System.Collections.Generic;

namespace ZerCreation.MapForces.MapCreator.Models
{
    [Serializable]
    public class Map
    {
        public IEnumerable<Point> Points { get; set; }
    }
}
