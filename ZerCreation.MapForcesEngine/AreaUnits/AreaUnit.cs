using ZerCreation.MapForcesEngine.AreaUnits.Types;
using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    public class AreaUnit : IUnit
    {
        public Coordinates Position { get; set; }
        public int Value { get; set; }
        public Player PlayerPossesion { get; set; }
        public AreaTypeBase Type { get; set; }

        public AreaUnit()
        {
            this.PlayerPossesion = null;
        }
    }
}
