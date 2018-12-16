using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    interface IArbiter
    {
        void SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget);
    }
}
