using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Enums;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Map.Cartographer;

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

        public IEnumerable<HashSet<AreaUnit>> Move(MoveOperation moveOperation)
        {
            // TODO: Check cartographer to assign Army.PlayerPosession
            // TODO: Check cartographer to assign Army.Units.Value

            this.trackCreator.SetupMovePaths(moveOperation);

            while (!moveOperation.CheckIfMoveIsFinished())
            {
                var nextPathPointsUnits = new HashSet<AreaUnit>();
                foreach (var unitTracker in moveOperation.UnitsTrackers)
                {
                    Coordinates newUnitPosition = unitTracker.MoveUnitToNextPathPoint();
                    AreaUnit resolvedAreaUnit = this.ResolveNewUnitPosition(newUnitPosition, moveOperation);
                    nextPathPointsUnits.Add(resolvedAreaUnit);
                    //this.NotifyAboutResolvation(resolvedAreaUnit);
                    moveOperation.Player.MovePoints--;
                }

                yield return nextPathPointsUnits;
            }
        }

        private AreaUnit ResolveNewUnitPosition(Coordinates newUnitPosition, MoveOperation moveOperation)
        {
            AreaUnit areaUnit = this.cartographer.FindAreaUnit(newUnitPosition);
            
            // call DiplomacyArbiter for each unit here
            // call BattleArbiter for each unit here
            if (moveOperation.Mode == MoveMode.PathOfConquer)
            {
                areaUnit.PlayerPossesion = moveOperation.Player;
            }

            return areaUnit;
        }

        private void NotifyAboutResolvation(AreaUnit resolvedAreaUnit)
        {
            // Call MoveOperation's notification event here
        }
    }
}
