using GameLib;
using GameLib.Games;
using GameLib.Tables;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace CardGame.Data
{
    public class BlackjackService
    {

        private readonly Blackjack Blackjack = new Blackjack();
        public readonly Player Dealer = new Player() { Name = "dealer" };
        private Hand DealerHand { get; set; }

        public BlackjackService()
        {
            Blackjack.AddPlayer(Dealer);
        }

        public Player JoinGame()
        {
            var newPlayer = new Player() { Name = "Player"};
            Blackjack.AddPlayer(newPlayer);
            return newPlayer;
        }

        public List<Hand> GetHands()
        {
            return Blackjack.PlayerHands;
        }

        public List<Hand> Deal()
        {
            Blackjack.PlayerHands.ForEach(hand => Blackjack.RemovePlayerHand(hand));
            Blackjack.Players.ForEach(player => Blackjack.AddPlayerHand(player));
            Blackjack.Shuffle();
            DealerHand = Blackjack.PlayerHands.FirstOrDefault(hand => hand.Player == Dealer);
            Blackjack.PlayerHands.ForEach(hand => Blackjack.DealToPlayer(2, hand));
            //hide the dealers 2nd card(in a real implementation this wouldn't go to the client)
            DealerHand.Cards.Last().IsVisible = false;

            return GetHands();
        }

        public int Hit(Hand hand)
        {
            Blackjack.DealToPlayer(1, hand);
            return Blackjack.GetBestScore(hand);
        }

        public HandOutcome GetHandOutcome(Hand hand)
        {
            return Blackjack.GetHandOutcome(hand, DealerHand);
        }

        public int GetHandScore(Hand hand)
        {
            return Blackjack.GetBestScore(hand);

        }

        public void Stay()
        {
            PlayDealerHand();
        }

        private void PlayDealerHand()
        {

            var dealerScore = Blackjack.GetBestScore(DealerHand);
            DealerHand.Cards.ForEach(card => card.IsVisible = true);
            while (0 < dealerScore && dealerScore < 17)
            {
                Blackjack.DealToPlayer(1, DealerHand);
                dealerScore = Blackjack.GetBestScore(DealerHand);
            }
        }

    }
}
