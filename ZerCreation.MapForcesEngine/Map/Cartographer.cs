using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map
{
    public class Cartographer
    {
        public List<AreaUnit> MapUnits { get; set; }

        public void UpdateUnitPossesion(List<AreaUnit> mapUnits, Player player)
        {
            mapUnits.ForEach(unit => unit.PlayerPossesion = player);
        }
    }
}
