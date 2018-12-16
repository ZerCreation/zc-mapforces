using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingUnit : IUnit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public Player PlayerPossesion { get; set; }
    }
}
