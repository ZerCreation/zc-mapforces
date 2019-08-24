using System;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Models
{
    [Serializable()]
    public class MapDescription
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<AreaUnit> AreaUnits { get; set; }
    }
}
