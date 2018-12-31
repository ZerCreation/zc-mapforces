using System.Collections.Generic;
using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Play
{
    public class Player
    {
        private readonly ArbiterDispatcher arbiterDispatcher;

        public int MovePoints { get; set; }

        public Player(ArbiterDispatcher arbiterDispatcher)
        {
            this.arbiterDispatcher = arbiterDispatcher;
        }

        public void Move(MoveOperation moveOperation)
        {
            this.arbiterDispatcher.SolveMove(moveOperation);
        }
    }
}
