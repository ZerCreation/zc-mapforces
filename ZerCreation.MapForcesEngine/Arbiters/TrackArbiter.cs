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
        private readonly Queue<Coordinates> path;

        public TrackArbiter()
        {
            this.path = new Queue<Coordinates>();
        }

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
                throw new NotSupportedException("Basic move operation is not supported yet.");
            }

            Army movingArmy = moveOperation.MovingArmy;
            Area areaTarget = moveOperation.AreaTarget;

            MovingUnit movingUnit = movingArmy.FetchNextUnit();
            AreaUnit areaUnit = areaTarget.FetchNextUnit();
            
            if (!this.path.Any())
            {
                Vector moveVector = areaUnit.Position - movingUnit.Position;
                this.PreparePath(movingUnit.Position, areaUnit.Position, moveVector);
            }

            Coordinates pointToMove = this.path.Dequeue();
            movingUnit.Position = pointToMove;
            movingArmy.PlayerPossesion.MovePoints--;

            return true;
        }

        private void PreparePath(Coordinates startPosition, Coordinates endPosition, Vector moveVector)
        {
            // Calculates to get ratio less than 1
            bool isMoreHorizontalMove = Math.Abs(moveVector.X) > Math.Abs(moveVector.Y);
            double directionRatio = 0;
            if (moveVector.X != 0 && moveVector.Y != 0)
            {
                directionRatio = isMoreHorizontalMove
                    ? Math.Abs((double)moveVector.Y / moveVector.X)
                    : Math.Abs((double)moveVector.X / moveVector.Y);
            }

            Coordinates prevPosition = startPosition;
            double shorterDirBuffer = 0;
            while (this.IsItFullPath(endPosition))
            {
                var newPosition = new Coordinates
                {
                    X = prevPosition.X,
                    Y = prevPosition.Y
                };

                if (isMoreHorizontalMove)
                {
                    if (shorterDirBuffer >= 1)
                    {
                        shorterDirBuffer--;
                        newPosition.Y += Math.Sign(moveVector.Y);
                    }
                    else
                    {
                        newPosition.X += Math.Sign(moveVector.X);
                        shorterDirBuffer += directionRatio;
                    }
                }
                else
                {
                    if (shorterDirBuffer >= 1)
                    {
                        shorterDirBuffer--;
                        newPosition.X += Math.Sign(moveVector.X);
                    }
                    else
                    {
                        newPosition.Y += Math.Sign(moveVector.Y);
                        shorterDirBuffer += directionRatio;
                    }
                }

                this.path.Enqueue(newPosition);
                prevPosition = newPosition;
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
