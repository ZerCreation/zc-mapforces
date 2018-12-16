using ZerCreation.MapForcesEngine.AreaUnits.Types;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class AreaUnit : IUnit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public Player PlayerPossesion { get; set; }
        public AreaTypeBase Type { get; set; }

        public AreaUnit()
        {
            this.PlayerPossesion = null;
        }
    }
}
