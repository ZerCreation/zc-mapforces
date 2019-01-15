using System;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class TrackArbiter : IArbiter
    {
        private Queue<Coordinates> path;

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
            
            Vector moveVector = areaUnit.Position - movingUnit.Position;
            if (this.path == null)
            {
                this.PreparePath(movingUnit.Position, moveVector);
            }

            Coordinates pointToMove = this.path.Dequeue();
            movingUnit.Position = pointToMove;
            movingArmy.PlayerPossesion.MovePoints--;

            return true;
        }

        private void PreparePath(Coordinates startPosition, Vector moveVector)
        {
            // TODO: Generate path for unit

            //movingUnit.X += Math.Sign(xDistance);
            //movingUnit.Y += Math.Sign(yDistance);
        }
    }
}
