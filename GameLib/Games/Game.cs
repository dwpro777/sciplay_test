using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Games
{
    public abstract class Game:Tables.Game
    {

        public List<Player> Players { get; private set; }
        protected List<Hand> PlayerHands { get; private set; }
        private IDeck Deck { get; set; }

        public Game()
        {
            Players = new List<Player>();
            PlayerHands = new List<Hand>();
        }

        public HandOutcome GetHandOutcome(Hand playerCards, Hand opponentCards)
        {
            var playerBestScore = GetBestScore(playerCards);
            var opponentBestScore = GetBestScore(opponentCards);
            
            if (playerBestScore > opponentBestScore)
                return HandOutcome.Win;
            else if (playerBestScore == opponentBestScore)
                return HandOutcome.Draw;
            else
                return HandOutcome.Lose;

        }

        protected void SetDeck(IDeck deck)
        {
            Deck = deck;
        }

        protected void Shuffle()
        {
            Deck.Shuffle();
        }

        protected void DealToPlayer(int NumberOfCards, Hand hand)
        {
            hand.Cards.AddRange(Deck.Deal(NumberOfCards));
        }

        protected void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        protected Hand AddPlayerHand(Player player)
        {
            var newHand = new Hand() { Player = player }; 
            PlayerHands.Add(newHand);
            return newHand;
        }

        protected void RemovePlayer(Player player)
        {
            PlayerHands = PlayerHands.Where(ph => ph.Player != player).ToList();
            Players.Remove(player);
        }

        public abstract GameType GetGameType();
        public abstract void CreateGame(int playerCount);
        public abstract void DealHand();

        public abstract int GetBestScore(Hand hand);
        public abstract void OutputHandResults();

    }
}
