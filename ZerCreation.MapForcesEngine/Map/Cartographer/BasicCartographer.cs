using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Map.Cartographer
{
    public class BasicCartographer : ICartographer
    {
        private List<AreaUnit> mapUnits;
        private List<IPlayer> players;

        public void SaveMapWorld(MapDescription mapDescription)
        {
            this.mapUnits = mapDescription.AreaUnits;

            this.players = this.mapUnits
                .Select(unit => unit.PlayerPossesion)
                .Where(_ => _ != null)
                .Distinct()
                .ToList();
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
            if (this.mapUnits == null)
            {
                throw new InvalidOperationException("Can't find any player because map hasn't been initialized yet.");
            }

            IPlayer player = this.players.FirstOrDefault(_ => _.Id == id);
            if (player == null)
            {
                throw new ArgumentException($"Player with Id = {id} wasn't found.");
            }

            return player;
        }
    }
}
