using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map
{
    public interface ICartographer
    {
        AreaUnit FindAreaUnit(Coordinates position);
    }
}