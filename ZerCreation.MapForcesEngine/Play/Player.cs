using System.Collections.Generic;
using ZerCreation.MapForcesEngine.Arbiters;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Play
{
    public class Player
    {
        private readonly ArbiterDispatcher arbiterDispatcher;

        public Player(ArbiterDispatcher arbiterDispatcher)
        {
            this.arbiterDispatcher = arbiterDispatcher;
        }

        public void Move(MovingArmy movingArmy, List<AreaUnit> areaTarget)
        {
            this.arbiterDispatcher.SolveMove(movingArmy, areaTarget);
        }
    }
}
