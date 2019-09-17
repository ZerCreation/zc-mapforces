using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Gameplay
{
    public class GameplayInitializer
    {
        private MapDescription mapDescription;
        private readonly ICartographer cartographer;
        private HashSet<IPlayer>.Enumerator playerInitEnumerator;

        public HashSet<IPlayer> Players { get; private set; }
        public List<AreaUnit> AreaUnits { get; private set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        public bool IsDone => this.mapDescription != null;

        public GameplayInitializer(ICartographer cartographer)
        {
            this.cartographer = cartographer;
        }

        public void InitializeMap(MapDescription mapDescription)
        {
            this.mapDescription = mapDescription;

            this.AreaUnits = mapDescription.AreaUnits;
            this.MapWidth = mapDescription.Width;
            this.MapHeight = mapDescription.Height;

            var players = mapDescription.AreaUnits
                .Select(unit => unit.PlayerPossesion)
                .Where(_ => _ != null)
                .Distinct();

            this.Players = new HashSet<IPlayer>(players);
            this.playerInitEnumerator = this.Players.GetEnumerator();

            this.cartographer.SaveMapDetails(this.AreaUnits, this.Players);
        }

        public IPlayer GetNextPlayerToInitialize()
        {
            this.playerInitEnumerator.MoveNext();
            return this.playerInitEnumerator.Current;
        }
    }
}
