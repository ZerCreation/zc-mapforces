using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Map
{
    public class TrackCreator
    {
        public void SetupMovePaths(MoveOperation moveOperation)
        {
            if (moveOperation.Mode != MoveMode.Basic)
            {
                throw new NotSupportedException("Basic move operation is not supported yet.");
            }

            Army movingArmy = moveOperation.MovingArmy;
            Area areaTarget = moveOperation.AreaTarget;

            if (moveOperation.Mode != MoveMode.Basic && movingArmy.Units.Count != areaTarget.Units.Count)
            {
                throw new ArgumentException("The number of army units differs from number of area units." +
                    "For Basic move operation it must be the same.");
            }

            // TODO: Improve it - use for() loop for Basic mode
            int idx = 0;
            foreach (MovingUnit movingUnit in movingArmy.Units)
            {
                //MovingUnit movingUnit = movingArmy.FetchNextUnit();
                AreaUnit areaUnit = areaTarget.Units[idx++];

                Queue<Coordinates> movePath = this.PreparePath(movingUnit.Position, areaUnit.Position);
                if (movePath.Last() != areaUnit.Position)
                {
                    throw new Exception("Generated move path doesn't contain right target.");
                }

                movingUnit.SetupMove(movePath);
            }
        }

        private Queue<Coordinates> PreparePath(Coordinates startPosition, Coordinates endPosition)
        {
            Vector moveVector = endPosition - startPosition;
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
            var path = new Queue<Coordinates>();

            while (this.IsItFullPath(path, endPosition))
            {
                var newPosition = new Coordinates
                {
                    X = prevPosition.X,
                    Y = prevPosition.Y
                };

                if (isMoreHorizontalMove)
                {
                    if (shorterDirBuffer < 1)
                    {
                        newPosition.X += Math.Sign(moveVector.X);
                        shorterDirBuffer += directionRatio;
                    }
                    else
                    {
                        newPosition.Y += Math.Sign(moveVector.Y);
                        shorterDirBuffer--;
                    }
                }
                else
                {
                    if (shorterDirBuffer < 1)
                    {
                        newPosition.Y += Math.Sign(moveVector.Y);
                        shorterDirBuffer += directionRatio;
                    }
                    else
                    {
                        newPosition.X += Math.Sign(moveVector.X);
                        shorterDirBuffer--;
                    }
                }

                path.Enqueue(newPosition);
                prevPosition = newPosition;
            }

            return path;
        }

        private bool IsItFullPath(Queue<Coordinates> path, Coordinates endPosition)
        {
            //const double targettingTolerance = 0.1; // Maybe not needed
            // TODO: Implement Coordinates.IsEqual()
            return !path.Any(_ => _.X == endPosition.X && _.Y == endPosition.Y);
        }
    }
}
