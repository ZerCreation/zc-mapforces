using System;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class TrackArbiter : IArbiter
    {
        /// <summary>
        /// Checks if move of <paramref name="movingArmy"/> can be done or not
        /// </summary>
        /// <param name="movingArmy">Army which is input and output as result</param>
        /// <param name="areaTarget">Area which is input and output as result</param>
        /// <returns>Returns false if there is no move possible (e.g. it's too far away)</returns>
        public bool SolveMove(MoveOperation moveOperation)
        {
            if (moveOperation.Mode != MoveMode.Basic)
            {
                throw new NotSupportedException("Not Basic move operation is not supported yet.");
            }

            Army movingArmy = moveOperation.MovingArmy;
            Area areaTarget = moveOperation.AreaTarget;

            MovingUnit movingUnit = movingArmy.FetchNextUnit();
            AreaUnit areaUnit = areaTarget.FetchNextUnit();

            // TODO: Improve
            int xDistance = areaUnit.X - movingUnit.X;
            int yDistance = areaUnit.Y - movingUnit.Y;

            movingUnit.X += Math.Sign(xDistance);
            movingUnit.Y += Math.Sign(yDistance);

            var currentPlayer = movingArmy.PlayerPossesion;
            currentPlayer.MovePoints--;

            return true;
        }
    }
}
