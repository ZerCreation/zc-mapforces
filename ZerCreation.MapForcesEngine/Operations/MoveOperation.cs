using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Operations
{
    public class MoveOperation
    {
        public Army MovingArmy { get; set; }
        public Area AreaTarget { get; set; }
        public MoveMode Mode { get; set; }

        public bool CheckIfMoveIsFinished()
        {
            return this.MovingArmy.PlayerPossesion.MovePoints == 0
                || this.MovingArmy.Units.All(unit => unit.IsMoveTargetReached);
        }
    }
}
