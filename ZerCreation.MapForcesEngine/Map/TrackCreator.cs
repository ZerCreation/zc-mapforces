using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Move;

namespace ZerCreation.MapForcesEngine.Map
{
    public class TrackCreator
    {
        public void SetupMovePaths(MoveOperation moveOperation)
        {
            switch (moveOperation.Mode)
            {
                case MoveMode.PathOfConquer:
                    // TODO: Use strategy pattern here
                    this.SetupBasicMovePaths(moveOperation);
                    break;
                default:
                    throw new NotSupportedException($"{moveOperation.Mode} isn't currently supported.");
            }
        }

        private void SetupBasicMovePaths(MoveOperation moveOperation)
        {
            Area sourceArea = moveOperation.SourceArea;
            Area targetArea = moveOperation.TargetArea;

            if (sourceArea.Units.Count != targetArea.Units.Count)
            {
                throw new ArgumentException("The number of army units differs from number of area units. " +
                    "For Basic move operation it must be the same.");
            }

            int unitsCount = sourceArea.Units.Count;
            for (int i = 0; i < unitsCount; i++)
            {
                AreaUnit sourceUnit = sourceArea.Units[i];
                AreaUnit targetUnit = targetArea.Units[i];

                Queue<Coordinates> movePath = this.PreparePath(sourceUnit.Position, targetUnit.Position);
                if (movePath.Last() != targetUnit.Position)
                {
                    throw new Exception("Generated move path doesn't contain right target.");
                }

                moveOperation.AddMovePath(movePath);
            }
        }

        private Queue<Coordinates> PreparePath(Coordinates startPosition, Coordinates endPosition)
        {
            Vector moveVector = endPosition - startPosition;
            // Calculates to get ratio less than 1
            bool isMoreHorizontalMove = Math.Abs(moveVector.X) > Math.Abs(moveVector.Y);
            float directionRatio = 0;
            if (moveVector.X != 0 && moveVector.Y != 0)
            {
                directionRatio = isMoreHorizontalMove
                    ? Math.Abs((float)moveVector.Y / moveVector.X)
                    : Math.Abs((float)moveVector.X / moveVector.Y);
            }

            Coordinates prevPosition = startPosition;
            float shorterDirBuffer = 0;
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
                    if ((shorterDirBuffer + 0.25 * directionRatio) < 0.5)
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
                    if ((shorterDirBuffer + 0.25 * directionRatio) < 0.5)
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
