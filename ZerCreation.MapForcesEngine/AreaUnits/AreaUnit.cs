using System;
using ZerCreation.MapForcesEngine.AreaUnits.Types;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    [Serializable()]
    public class AreaUnit : IUnit
    {
        public Coordinates Position { get; set; }
        public IPlayer PlayerPossesion { get; set; }
        public AreaTypeBase Type { get; set; }

        public AreaUnit(int initX, int initY)
            : this(new Coordinates(initX, initY))
        {
        }

        public AreaUnit(Coordinates initPosition)
        {
            this.Position = initPosition;
        }

        public override string ToString()
        {
            return $"Position: {this.Position}";
        }
    }
}
