using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Map.Cartographer;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Turns
{
    public class TurnService
    {
        private readonly HashSet<IPlayer> allPlayers;
        private readonly HashSet<IPlayer> playersUsedInCurrentRound;

        public IPlayer CurrentPlayer { get; set; }

        public TurnService()
        {
            this.allPlayers = new HashSet<IPlayer>();
            this.playersUsedInCurrentRound = new HashSet<IPlayer>();
        }

        public void SwitchToNextPlayer()
        {
            if (this.playersUsedInCurrentRound.Count == this.allPlayers.Count)
            {
                this.SwitchToNextRound();
            }

            this.SwitchTurn();
        }

        private void SwitchToNextRound()
        {
            this.playersUsedInCurrentRound.Clear();
        }

        private void SwitchTurn()
        {
            IPlayer nextTurnPlayer = null;
            do
            {
                nextTurnPlayer = this.GetRandomPlayer();
            }
            while (this.playersUsedInCurrentRound.Contains(nextTurnPlayer));

            this.playersUsedInCurrentRound.Add(nextTurnPlayer);
            this.CurrentPlayer = nextTurnPlayer;
        }

        public void ValidatePlayerCanMove(Guid playerId)
        {
            IPlayer player = this.allPlayers.SingleOrDefault(_ => _.Id == playerId);

            if (player?.Id != this.CurrentPlayer.Id)
            {
                throw new WrongPlayerTurnException();
            }
        }

        public IPlayer GetRandomPlayer()
        {
            var random = new Random();
            int playerIdx = random.Next(this.allPlayers.Count);

            return this.allPlayers.ToList()[playerIdx];
        }
    }
}
