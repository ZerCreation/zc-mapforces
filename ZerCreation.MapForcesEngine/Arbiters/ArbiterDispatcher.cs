using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class ArbiterDispatcher
    {
        private List<IArbiter> arbiters;

        public ArbiterDispatcher()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.arbiters = new List<IArbiter>
            {
                new TrackArbiter(),
                new DiplomacyArbiter(),
                new GeographyArbiter(),
                new BattleArbiter()
            };
        }

        internal void SolveMove(MoveOperation moveOperation)
        {
            Army movingArmyBeforeMove = null; // movingArmy.Clone();
            Area areaTargetBeforeMove = null; // areaTarget.Clone();

            while (!moveOperation.CheckIfMoveIsFinished())
            {
                foreach (IArbiter arbiter in arbiters)
                {
                    bool arbitionResult = arbiter.SolveMove(moveOperation);
                    if (!arbitionResult)
                    {
                        moveOperation.MovingArmy = movingArmyBeforeMove;
                        moveOperation.AreaTarget = areaTargetBeforeMove;
                        return;
                    }
                } 
            }
        }
    }
}
