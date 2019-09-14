using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public class BasicCartographer : ICartographer
    {
        private List<AreaUnit> mapUnits;

        public void SaveMapWorld(MapDescription mapDescription)
        {
            this.mapUnits = mapDescription.AreaUnits;
        }

        public AreaUnit FindAreaUnit(Coordinates position)
        {
            if (this.mapUnits == null)
            {
                throw new InvalidOperationException("Can't find any area unit because map hasn't been initialized yet.");
            }

            return this.mapUnits.Single(mapUnit => mapUnit.Position == position);
        }
    }
}
