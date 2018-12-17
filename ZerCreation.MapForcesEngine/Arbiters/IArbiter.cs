using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    interface IArbiter
    {
        bool SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget);
    }
}
