using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForcesEngine.Move
{
    public class MoveUnitTracker
    {
        private Queue<Coordinates> movePath;        
        private Coordinates pathTarget;
        private Coordinates unitPosition;

        public bool IsMoveTargetReached
        {
            get
            {
                return this.unitPosition == this.pathTarget;
            }
        }

        public MoveUnitTracker(Queue<Coordinates> movePath)
        {
            this.movePath = movePath;

            this.unitPosition = movePath.First();
            this.pathTarget = movePath.Last();
        }

        internal Coordinates MoveUnitToNextPathPoint()
        {
            if (!this.movePath.Any())
            {
                throw new InvalidOperationException("There is no planned path for further moving.");
            }

            Coordinates pointToMove = this.movePath.Dequeue();
            this.unitPosition = pointToMove;

            return this.unitPosition;
        }
    }
}
