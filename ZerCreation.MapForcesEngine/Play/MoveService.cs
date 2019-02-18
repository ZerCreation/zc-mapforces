using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Operations;

namespace ZerCreation.MapForcesEngine.Play
{
    public class MoveService
    {
        private readonly TrackCreator trackCreator;
        private readonly Cartographer cartographer;

        public MoveService(TrackCreator trackCreator, Cartographer cartographer)
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
                    // call DiplomacyArbiter for each unit here
                    // call BattleArbiter for each unit here

                    // update area possesion after each move
                }
            }

            this.cartographer.UpdateUnitPossesion(
                moveOperation.AreaTarget.Units, 
                moveOperation.MovingArmy.PlayerPossesion);
        }
}
}
