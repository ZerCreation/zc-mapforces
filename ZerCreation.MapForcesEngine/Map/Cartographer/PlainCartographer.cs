using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public class PlainCartographer : ICartographer
    {
        private List<AreaUnit> mapUnits;

        public void DrawMap(MapDescription mapDescription)
        {
            this.mapUnits = new List<AreaUnit>();

            for (int x = 0; x < mapDescription.Width; x++)
            {
                for (int y = 0; y < mapDescription.Height; y++)
                {
                    this.mapUnits.Add(new AreaUnit(x, y));
                }
            }
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
