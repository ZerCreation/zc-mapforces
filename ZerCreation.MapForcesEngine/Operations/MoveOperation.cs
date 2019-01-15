using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Operations
{
    public class MoveOperation
    {
        public Army MovingArmy { get; set; }
        public Area AreaTarget { get; set; }
        public MoveMode Mode { get; set; }

        public bool MoveIsFinished
        {
            get
            {
                return this.MovingArmy.PlayerPossesion.MovePoints == 0;
                // TODO: Or check if units achieved target area
            }
        }
    }
}
