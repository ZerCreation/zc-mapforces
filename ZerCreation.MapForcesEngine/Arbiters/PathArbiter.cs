using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class PathArbiter : IArbiter
    {
        /// <summary>
        /// Checks if move of <paramref name="movingArmy"/> can be done or not
        /// </summary>
        /// <param name="movingArmy">Army which is input and output as result</param>
        /// <param name="areaTarget">Area which is input and output as result</param>
        /// <returns>Returns false if there is no move possible</returns>
        public bool SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget)
        {
            throw new System.NotImplementedException();
        }
    }
}
