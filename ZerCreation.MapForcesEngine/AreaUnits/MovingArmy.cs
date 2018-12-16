using System.Collections.Generic;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingArmy
    {
        public List<MovingUnit> Units { get; set; }
        public Player PlayerPossesion { get; set; }
    }
}
