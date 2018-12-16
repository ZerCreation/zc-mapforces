using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class GeographyArbiter : IArbiter
    {
        public void SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget)
        {
            //.Where(unit => unit.Type is Earth)
            //    .Where(unit => unit.Type is WaterCorridor)
        }
    }
}
