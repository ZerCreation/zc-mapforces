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
            return (this.MovingArmy.PlayerPossesion.MovePoints == 0)
                || (this.IsAllWholeArmyInTarget());
        }

        private bool IsAllWholeArmyInTarget()
        {
            foreach (MovingUnit armyUnit in this.MovingArmy.Units)
            {
                bool isInTarget = this.AreaTarget.Units.Any(areaUnit => armyUnit.Position == areaUnit.Position);
                if (!isInTarget)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
