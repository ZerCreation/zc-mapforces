using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Play
{
    public class MoveService
    {
        private readonly TrackCreator trackCreator;
        private readonly ICartographer cartographer;

        public MoveService(TrackCreator trackCreator, ICartographer cartographer)
        {
            this.trackCreator = trackCreator;
            this.cartographer = cartographer;
        }

        public void Move(MoveOperation moveOperation)
        {
            // TODO: Check cartographer to assign Army.PlayerPosession
            // TODO: Check cartographer to assign Army.Units.Value

            this.trackCreator.SetupMovePaths(moveOperation);

            while (!moveOperation.CheckIfMoveIsFinished())
            {
                Army movingArmy = moveOperation.MovingArmy;
                foreach (MovingUnit unit in movingArmy.Units)
                {
                    unit.MoveToNextPathPoint();
                    movingArmy.PlayerPossesion.MovePoints--;

                    AreaUnit areaUnit = this.cartographer.FindAreaUnit(unit.Position);
                    // call DiplomacyArbiter for each unit here
                    // call BattleArbiter for each unit here

                    // update area possession if needed
                    areaUnit.PlayerPossesion = movingArmy.PlayerPossesion;
                }
            }
        }
}
}
