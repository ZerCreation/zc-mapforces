using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Map
{
    public class Cartographer : ICartographer
    {
        private readonly List<AreaUnit> mapUnits;

        public Cartographer(/*List<AreaUnit> mapUnits*/)
        {
            //this.mapUnits = mapUnits;
        }

        public AreaUnit FindAreaUnit(Coordinates position)
        {
            return this.mapUnits.Single(mapUnit => mapUnit.Position == position);
        }
    }
}
