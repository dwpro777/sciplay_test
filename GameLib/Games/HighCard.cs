using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Games
{
    public class HighCard: CardGame
    {
        public override GameType GetGameType() => GameType.HighCard;
        private const int numberOfCardsToDeal = 1; 

        public override int GetBestScore(Hand hand)
        {
            var potentialOutcomes = new List<int>() { 0 };
            
            foreach(Card card in hand.Cards)
            {
                //bump up ace value for highcard
                var cardValue = card.Name == CardName.Ace ? 14 : (int)card.Name;
                potentialOutcomes.Add(cardValue);

            }

            return potentialOutcomes.Any() ? potentialOutcomes.Max() : -1;
        }

        public override void DealHand()
        {
                //give each player one card 
                foreach(var index in Enumerable.Range(0, numberOfCardsToDeal))
                {
                    foreach(var hand in PlayerHands)
                    {
                        DealToPlayer(1, hand);
                    }
                }
        }

        public override void OutputHandResults()
        {
                //Determine winners
                foreach(var hand in PlayerHands)
                {
                    foreach(var opponentHand in PlayerHands.Where( h => h!=hand))
                    {
                        var handOutcome = GetHandOutcome(hand, opponentHand);
                        Debug.WriteLine($"outcome: {hand.Player.Name} vs {opponentHand.Player.Name} card {hand.Cards[0].Name }  { handOutcome} against {opponentHand.Cards[0].Name} ");
                    }
                }
        }
    }
}
