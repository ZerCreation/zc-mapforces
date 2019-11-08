using System;
using System.Collections.Generic;
using System.Linq;
using ZerCreation.MapForcesEngine.Play;

namespace ZerCreation.MapForcesEngine.Turns
{
    public class TurnService
    {
        private HashSet<IPlayer> allPlayers;
        private HashSet<IPlayer> playersUsedInCurrentRound;

        public int RoundId { get; private set; } = 1;
        public IPlayer CurrentPlayer { get; set; }
        
        public void Setup(HashSet<IPlayer> allPlayers)
        {
            this.allPlayers = allPlayers;
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
            this.RoundId++;
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

        public void ValidateItsPlayerTurn(Guid playerId)
        {
            IPlayer player = this.allPlayers.SingleOrDefault(_ => _.Id == playerId);
            if (player == null || player.Id != this.CurrentPlayer?.Id)
            {
                throw new WrongPlayerTurnException();
            }
        }

        private IPlayer GetRandomPlayer()
        {
            var random = new Random();
            int playerIdx = random.Next(this.allPlayers.Count);

            return this.allPlayers.ToList()[playerIdx];
        }
    }
}
