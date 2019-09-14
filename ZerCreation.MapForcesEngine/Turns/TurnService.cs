using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Models;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Turns
{
    public class TurnService
    {
        private List<IPlayer> players;
        private Random playerOrderRandomizer;

        public IPlayer CurrentPlayer { get; set; }

        public TurnService()
        {
            this.playerOrderRandomizer = new Random();
        }

        public void SwitchToNextTurn()
        {
            int nextPlayerInTurn = this.playerOrderRandomizer.Next(0, this.players.Count);
            this.CurrentPlayer = this.players[nextPlayerInTurn];
        }

        public void SetupPlayers(MapDescription mapDescription)
        {
            this.players = mapDescription.AreaUnits
                .Select(unit => unit.PlayerPossesion)
                .Where(_ => _ != null)
                .Distinct()
                .ToList();

            this.SwitchToNextTurn();
        }

        public void ValidatePlayerCanMove(Guid playerId)
        {
            IPlayer player = this.FindPlayerById(playerId);

            if (player.Id != this.CurrentPlayer.Id)
            {
                throw new WrongPlayerTurnException();
            }
        }

        private IPlayer FindPlayerById(Guid id)
        {
            if (this.players == null)
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
