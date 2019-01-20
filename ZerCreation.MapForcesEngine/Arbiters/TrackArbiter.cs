using System;
using System.Collections.Generic;
using System.Linq;
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
                this.PreparePath(movingUnit.Position, areaUnit.Position, moveVector);
            }

            Coordinates pointToMove = this.path.Dequeue();
            movingUnit.Position = pointToMove;
            movingArmy.PlayerPossesion.MovePoints--;

            return true;
        }

        private void PreparePath(Coordinates startPosition, Coordinates endPosition, Vector moveVector)
        {
            this.path = new Queue<Coordinates>();

            // Calculates to get ratio less than 1
            bool isMoreHorizontalMove = moveVector.X > moveVector.Y;
            double directionRatio = isMoreHorizontalMove
                ? Math.Abs(moveVector.Y / moveVector.X)
                : Math.Abs(moveVector.X / moveVector.Y);

            double shorterDirBuffer = 0;
            while (this.IsItFullPath(endPosition))
            {
                Coordinates prevPosition = this.path.Any() 
                    ? this.path.Peek() 
                    : startPosition;

                var newPosition = new Coordinates
                {
                    X = prevPosition.X,
                    Y = prevPosition.Y
                };
                shorterDirBuffer += directionRatio;

                if (isMoreHorizontalMove)
                {
                    newPosition.X += Math.Sign(moveVector.X);
                    if (shorterDirBuffer >= 1)
                    {
                        shorterDirBuffer--;
                        newPosition.Y += Math.Sign(moveVector.Y);
                    }
                }
                else
                {
                    newPosition.Y += Math.Sign(moveVector.Y);
                    if (shorterDirBuffer >= 1)
                    {
                        shorterDirBuffer--;
                        newPosition.X += Math.Sign(moveVector.X);
                    }
                }

                this.path.Enqueue(newPosition);
            }
        }

        private bool IsItFullPath(Coordinates endPosition)
        {
            //const double targettingTolerance = 0.1; // Maybe not needed
            // Implement Coordinates.IsEqual()
            return !this.path.Any(_ => _.X == endPosition.X && _.Y == endPosition.Y);
        }
    }
}
