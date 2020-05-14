using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Games
{
    public class Blackjack : Game
    {
        private readonly CardName[] Facecards = { CardName.Jack, CardName.Queen, CardName.King };
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

        //<TODO> handle splits
        //Consider, who can see which cards, who is playing against whom (ie, is everyone playing against the house)
        //how should dealing rules be handled, since there would need to be interaction

    }
}
