using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public interface ICartographer
    {
        AreaUnit FindAreaUnit(Coordinates position);
        void DrawMap(MapDescription mapDescription);
    }
}