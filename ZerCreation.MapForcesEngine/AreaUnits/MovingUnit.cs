using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingUnit : IUnit
    {
        public Coordinates Position { get; set; }
        public int Value { get; set; }
    }
}
