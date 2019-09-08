using System;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public interface ICartographer
    {
        void SaveMapWorld(MapDescription mapDescription);
        AreaUnit FindAreaUnit(Coordinates position);
        IPlayer FindPlayerById(Guid id);
    }
}