using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.AreaUnits;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;
using ZerCreation.MapForcesEngine.Turns;

namespace ZerCreation.MapForcesEngine.Gameplay
{
    public class GameplayInitializer
    {
        private MapDescription mapDescription;
        private readonly ICartographer cartographer;
        private readonly TurnService turnService;
        private HashSet<IPlayer>.Enumerator playerInitEnumerator;
        private HashSet<IPlayer> initializingPlayers;

        public HashSet<IPlayer> Players { get; private set; }
        public List<AreaUnit> AreaUnits { get; private set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        public bool IsMapInitialized => this.mapDescription != null;
        public bool HaveAllPlayersJoined => this.initializingPlayers.Count == this.Players.Count;

        public GameplayInitializer(ICartographer cartographer, TurnService turnService)
        {
            this.cartographer = cartographer;
            this.turnService = turnService;

            this.initializingPlayers = new HashSet<IPlayer>();
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
            this.turnService.Setup(this.Players);
        }

        public IPlayer GetNextPlayerToInitialize()
        {
            this.playerInitEnumerator.MoveNext();
            IPlayer initPlayer = this.playerInitEnumerator.Current;
            this.initializingPlayers.Add(initPlayer);

            return initPlayer;
        }
    }
}
