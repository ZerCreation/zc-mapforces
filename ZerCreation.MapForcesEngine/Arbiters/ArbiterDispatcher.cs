using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Arbiters
{
    public class ArbiterDispatcher
    {
        private readonly Cartographer cartographer;
        private List<IArbiter> arbiters;

        public ArbiterDispatcher(Cartographer cartographer)
        {
            this.cartographer = cartographer;

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

            while (!moveOperation.MoveIsFinished)
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

            List<AreaUnit> newAreaTarget = null;
            Player player = moveOperation.MovingArmy.PlayerPossesion;

            this.cartographer.UpdateUnitPossesion(newAreaTarget, player);
        }
    }
}
