using ZerCreation.MapForcesEngine.Map;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class MovingUnit : IUnit
    {
        public Coordinates Position { get; set; }
        public int Value { get; set; }

        public MovingUnit(int initX, int initY)
            : this(new Coordinates(initX, initY))
        {
        }

        public MovingUnit(Coordinates initPosition)
        {
            this.Position = initPosition;
        }
    }
}
