using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Move
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
            IPlayer player = moveOperation.Player;

            while (!moveOperation.CheckIfMoveIsFinished())
            {
                foreach (var unitTracker in moveOperation.UnitsTrackers)
                {
                    Coordinates newUnitPosition = unitTracker.MoveUnitToNextPathPoint();
                    AreaUnit resolvedAreaUnit = this.ResolveNewUnitPosition(newUnitPosition, player);
                    this.NotifyAboutResolvation(resolvedAreaUnit);
                    player.MovePoints--;
                }
            }
        }

        private AreaUnit ResolveNewUnitPosition(Coordinates newUnitPosition, IPlayer player)
        {
            AreaUnit areaUnit = this.cartographer.FindAreaUnit(newUnitPosition);
            // call DiplomacyArbiter for each unit here
            // call BattleArbiter for each unit here

            // update area possession if needed
            //areaUnit.PlayerPossesion = player;

            return areaUnit;
        }

        private void NotifyAboutResolvation(AreaUnit resolvedAreaUnit)
        {
            // Call MoveOperation's notification event here
        }
    }
}
