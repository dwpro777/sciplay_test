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
    public class Blackjack : CardGame
    {
        private readonly CardName[] Facecards = { CardName.Jack, CardName.Queen, CardName.King };
        private const int numberOfCardsToDeal = 2; 
        public override GameType GetGameType() => GameType.Blackjack;

        public override int GetBestScore(Hand hand)
        {
            var potentialOutcomes = new List<int>() { 0 };
            
            foreach(Card card in hand.Cards)
            {
                var cardValue = Facecards.Contains(card.Name) ? 10 : (int)card.Name;

                var newOutcomes = new List<int>();
                foreach(var outcomeIndex in Enumerable.Range(0, potentialOutcomes.Count))
                {
                    if(card.Values.Count() > 1 )
                    {
                        foreach(var cardIndex in Enumerable.Range(0, card.Values.Count()))
                        {
                            newOutcomes.Add(potentialOutcomes[outcomeIndex] + card.Values[cardIndex]);
                        }
                    }
                    else
                        potentialOutcomes[outcomeIndex] += cardValue;
                }
                if (newOutcomes.Any())
                    potentialOutcomes = newOutcomes;
            }

            //get the highest value that's not a bust
            var bestScoreQuery = potentialOutcomes.Where(o => o < 22);
            //return value, -1 for bust
            return bestScoreQuery.Any() ? bestScoreQuery.Max() : -1;
        }

        public override void DealHand()
        {
                //give each player two cards
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
                        Debug.WriteLine($"outcome: {hand.Player.Name} vs {opponentHand.Player.Name} card {hand.Cards[0].Name }/{hand.Cards[1].Name}  { handOutcome} against {opponentHand.Cards[0].Name}/{opponentHand.Cards[1].Name}  ");
                    }
                }
        }

        //<TODO> handle splits

    }
}
