using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingUnit : IUnit
    {
        private Queue<Coordinates> movePath;
        private Coordinates pathTarget;

        public Coordinates Position { get; set; }
        public int Force { get; set; }
        public bool IsMoveTargetReached
        {
            get
            {
                return this.Position == this.pathTarget;
            }
        }

        public MovingUnit(int initX, int initY)
            : this(new Coordinates(initX, initY))
        {
        }

        public MovingUnit(Coordinates initPosition)
        {
            this.Position = initPosition;
            this.movePath = new Queue<Coordinates>();
        }

        internal void SetupMove(Queue<Coordinates> movePath)
        {
            this.movePath = movePath;
            this.pathTarget = movePath.Last();
        }

        internal void MoveToNextPathPoint()
        {
            if (!this.movePath.Any())
            {
                throw new InvalidOperationException("There is no planned path for further moving.");
            }

            Coordinates pointToMove = this.movePath.Dequeue();
            this.Position = pointToMove;
        }

        public override string ToString()
        {
            return $"Position: {this.Position}, Force: {this.Force}";
        }
    }
}
