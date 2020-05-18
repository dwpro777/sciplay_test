using GameLib;
using GameLib.Games;
using GameLib.Tables;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace CardGame.Data
{

    public class BindingObject
    {
        public Player Player { get; set; }
        public List<Hand> PlayerHands { get; set; }
        public Hand DealerHand { get; set; }
        public bool HasWinner = false;
        public GameLib.HandOutcome HandOutcome;
    } 

    public class BlackjackService
    {

        private readonly Blackjack Blackjack = new Blackjack();
        public readonly Player Dealer = new Player() { Name = "Dealer" };
        private Hand DealerHand { get; set; }
        public BindingObject Bindings { get; private set; }

        public BlackjackService()
        {
            Blackjack.AddPlayer(Dealer);
            Bindings = new BindingObject() { };
        }

        public BindingObject JoinGame()
        {
            var newPlayer = new Player() { Name = "Player"};
            Blackjack.AddPlayer(newPlayer);
            Bindings.Player = newPlayer;
            return Bindings;

        }

        public List<Hand> GetHands()
        {
            return Blackjack.PlayerHands;
        }

        public void Deal()
        {
            Bindings.HasWinner = false;
            Blackjack.PlayerHands.ForEach(hand => Blackjack.RemovePlayerHand(hand));
            Blackjack.Players.ForEach(player => Blackjack.AddPlayerHand(player));
            Blackjack.Shuffle();
            DealerHand = Blackjack.PlayerHands.FirstOrDefault(hand => hand.Player == Dealer);
            Bindings.DealerHand = DealerHand;
            Bindings.PlayerHands= Blackjack.PlayerHands.Where(hand => hand != DealerHand).ToList();

            Blackjack.PlayerHands.ForEach(hand => Blackjack.DealToPlayer(2, hand));

            CheckForWinner();
            if(!Bindings.HasWinner)
                DealerHand.Cards.Last().IsVisible = false;
            //hide the dealers 2nd card(in a real implementation this wouldn't go to the client)

        }

        public void Hit(int handIndex = 0)
        {
            if (Bindings.PlayerHands.Count >= handIndex)
            {
                var currentHand = Bindings.PlayerHands[handIndex];
                Blackjack.DealToPlayer(1, currentHand);
                CheckForWinner(handIndex);
            }

        }

        private void CheckForWinner(int handIndex = 0, bool playerIsStaying = false)
        {
            if (Bindings.PlayerHands.Count >= handIndex)
            {
                var currentHand = Bindings.PlayerHands[handIndex];

                var score = Blackjack.GetBestScore(currentHand);
                if (score == -1)
                {
                    Bindings.HandOutcome = HandOutcome.Lose;
                    Bindings.HasWinner = true;
                    return;
                }
                else if(currentHand.Cards.Count == 2 && score == 21)
                {
                    Bindings.HandOutcome = HandOutcome.Win;
                    Bindings.HasWinner = true;
                }
                else if (playerIsStaying)
                {
                    Bindings.HandOutcome = Blackjack.GetHandOutcome(currentHand, DealerHand);
                    Bindings.HasWinner= true;
                }

            }
        }

        public int GetHandScore(Hand hand)
        {
            return Blackjack.GetBestScore(hand);
        }

        public void Stay(int handIndex = 0)
        {
            PlayDealerHand();
            CheckForWinner(handIndex, true);
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
