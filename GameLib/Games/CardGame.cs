using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib.Games
{
    public abstract class CardGame :Game
    {
        public List<Hand> PlayerHands { get; private set; }
        private IDeck Deck { get; set; }
        public abstract void DealHand();
        public abstract int GetBestScore(Hand hand);
        public abstract void OutputHandResults();

        public CardGame()
        {
            PlayerHands = new List<Hand>();
            var standardDeck = new StandardDeck();
            SetDeck(standardDeck);
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

        public void Shuffle()
        {
            Deck.Shuffle();
        }

        public void DealToPlayer(int NumberOfCards, Hand hand)
        {
            hand.Cards.AddRange(Deck.Deal(NumberOfCards));
        }

        public override void RemovePlayer(Player player)
        {
            base.RemovePlayer(player);
            PlayerHands = PlayerHands.Where(ph => ph.Player != player).ToList();

        }

        public override void CreateGame(int playerCount)
        {
            base.CreateGame(playerCount);
            Shuffle();
            foreach(var player in Players)
            {
                AddPlayerHand(player);
            }

        }

        public Hand AddPlayerHand(Player player)
        {
            var newHand = new Hand() { Player = player }; 
            PlayerHands.Add(newHand);
            return newHand;
        }

        public void RemovePlayerHand(Hand hand)
        {
            PlayerHands = PlayerHands.Where( h => h != hand).ToList();
        }


    }
}
