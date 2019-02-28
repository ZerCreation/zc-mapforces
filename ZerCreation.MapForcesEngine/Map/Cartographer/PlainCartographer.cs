using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public class PlainCartographer : ICartographer
    {
        private List<AreaUnit> mapUnits;

        public void DrawMap(MapSettings mapSettings)
        {
            this.mapUnits = new List<AreaUnit>();

            for (int x = 0; x < mapSettings.Width; x++)
            {
                for (int y = 0; y < mapSettings.Height; y++)
                {
                    this.mapUnits.Add(new AreaUnit(x, y));
                }
            }
        }

        public AreaUnit FindAreaUnit(Coordinates position)
        {
            return this.mapUnits.Single(mapUnit => mapUnit.Position == position);
        }
    }
}
