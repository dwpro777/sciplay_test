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
        public override GameType GetGameType() => GameType.HighCard;

        //Question: How to enforce 'rules' (ie, who would control dealing, should that be enforced by the game class?)

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
