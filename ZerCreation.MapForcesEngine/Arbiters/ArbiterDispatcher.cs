using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
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
                new DiplomacyArbiter(),
                new GeographyArbiter(),
                new BattleArbiter()
            };
        }

        internal void SolveMove(MovingArmy movingArmy, List<AreaUnit> areaTarget)
        {
            foreach (IArbiter arbiter in arbiters)
            {
                arbiter.SolveMove(movingArmy, areaTarget);
            }

            List<AreaUnit> newAreaTarget = null;
            Player player = movingArmy.PlayerPossesion;

            this.cartographer.UpdateUnitPossesion(newAreaTarget, player);
        }
    }
}
