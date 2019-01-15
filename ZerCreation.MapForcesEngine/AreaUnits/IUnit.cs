using ZerCreation.MapForcesEngine.Map;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    interface IUnit
    {
        Coordinates Position { get; set; }
        int Value { get; set; }
        Player PlayerPossesion { get; set; }
    }
}
