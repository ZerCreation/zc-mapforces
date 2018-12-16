using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.AreaUnits
{
    interface IUnit
    {
        int X { get; set; }
        int Y { get; set; }
        int Value { get; set; }
        Player PlayerPossesion { get; set; }
    }
}
