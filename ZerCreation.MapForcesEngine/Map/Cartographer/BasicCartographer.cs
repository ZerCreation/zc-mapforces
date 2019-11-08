using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public class BasicCartographer : ICartographer
    {
        private List<AreaUnit> mapUnits;
        private HashSet<IPlayer> mapPlayers;

        public void SaveMapDetails(List<AreaUnit> areaUnits, HashSet<IPlayer> players)
        {
            this.mapUnits = areaUnits;
            this.mapPlayers = players;
        }

        public AreaUnit FindAreaUnit(Coordinates position)
        {
            if (this.mapUnits == null)
            {
                throw new InvalidOperationException("Can't find any area unit because map hasn't been initialized yet.");
            }

            return this.mapUnits.Single(mapUnit => mapUnit.Position == position);
        }

        public IPlayer FindPlayerById(Guid id)
        {
            if (this.mapPlayers == null)
            {
                throw new InvalidOperationException("Can't find any player because map hasn't been initialized yet.");
            }

            IPlayer player = this.mapPlayers.FirstOrDefault(_ => _.Id == id);
            if (player == null)
            {
                throw new ArgumentException($"Player with Id = {id} wasn't found.");
            }

            return player;
        }
    }
}
