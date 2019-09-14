using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public interface ICartographer
    {
        void SaveMapWorld(MapDescription mapDescription);
        AreaUnit FindAreaUnit(Coordinates position);
    }
}