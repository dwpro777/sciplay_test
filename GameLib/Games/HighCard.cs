using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Games
{
    public class HighCard: Game
    {
        private readonly CardName[] facecards = { CardName.Jack, CardName.Queen, CardName.King };
        public override GameType GetGameType() => GameType.HighCard;

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
    }
}
