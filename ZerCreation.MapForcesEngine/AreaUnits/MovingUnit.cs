using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingUnit : IUnit
    {
        public Coordinates Position { get; set; }
        public int Value { get; set; }
        public Player PlayerPossesion { get; set; }
    }
}
