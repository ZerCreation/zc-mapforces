using System;
using System.Collections.Generic;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public interface ICartographer
    {
        void SaveMapDetails(List<AreaUnit> areaUnits, HashSet<IPlayer> players);
        AreaUnit FindAreaUnit(Coordinates position);
        IPlayer FindPlayerById(Guid id);
    }
}