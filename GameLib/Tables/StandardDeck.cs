using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace GameLib.Tables
{
    public class StandardDeck : IDeck
    {
        public List<Card> Cards { get; private set; }
        public int CardIndex { get; private set; }
        public StandardDeck()
        {
            GetCards();
            CardIndex = 0;
        }

        private void GetCards()
        {
            Cards = new List<Card>();
            foreach (CardSuit CardSuit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardName cardName in Enum.GetValues(typeof(CardName)))
                {
                    var newCard = new GameLib.Tables.Card() { Name = cardName, Suit = CardSuit };
                    if (cardName == CardName.Ace)
                    {
                        newCard.Values = new int[] { 1, 11 };
                    }
                    else
                    {
                        newCard.Values = new int[] { (int)cardName };
                    }
                    
                    if((int) cardName >1 && (int) cardName < 11)
                    {
                        newCard.fileName = $"images/{(int)cardName}";
                    }
                    else
                        newCard.fileName = $"images/{cardName.ToString().Substring(0,1)}";

                    newCard.fileName += $"{CardSuit.ToString().Substring(0,1)}.png";

                    Cards.Add(newCard);
                }
            }

        }

        public void Shuffle()
        {
            if (Cards.Any())
            {
                Cards = Cards.OrderBy(c => Guid.NewGuid()).ToList();
            }
            CardIndex = 0;
        }

        public List<Card> Deal(int cardCount)
        {
            var dealtCards = new List<Card>();
            foreach (var cardDealIndex in Enumerable.Range(0, cardCount))
            {
                if (cardDealIndex + CardIndex < Cards.Count)
                {
                    dealtCards.Add(Cards[CardIndex]);
                    CardIndex++;
                }
                else
                    throw new IndexOutOfRangeException();
            }
            return dealtCards;
        }
    }
}


