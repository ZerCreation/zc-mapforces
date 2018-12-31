using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Operations
{
    public class MoveOperation
    {
        public Army MovingArmy { get; set; }
        public Area AreaTarget { get; set; }
        public MoveMode Mode { get; set; }
    }
}
