using ZerCreation.MapForcesEngine.AreaUnits;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public interface ICartographer
    {
        AreaUnit FindAreaUnit(Coordinates position);
        void DrawMap(MapDescription mapDescription);
    }
}