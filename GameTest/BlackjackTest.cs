using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GameLib.Tables;
using System.Collections.Generic;
using GameLib.Games;
using System.Diagnostics;

namespace GameTest
{
    [TestClass]
    public class BlackjackTest
    {

        public class BlackJackTestHand
        {
            public List<CardName> PlayerHand { get; set; }
            public List<CardName> OpponentHand { get; set; }
            public HandOutcome HandOutcome { get; set; }
            public BlackJackTestHand(List<CardName> playerHand, List<CardName> opponentHand, HandOutcome handOutcome)
            {
                PlayerHand = playerHand;
                OpponentHand = opponentHand;
                HandOutcome = handOutcome;
            }

            public static Hand GetPlayerHand(List<CardName> cardNames, StandardDeck deck)
            {
                var hand= new Hand();
                foreach (var cardName in cardNames)
                    hand.Cards.Add(deck.Cards.First(c => c.Name == cardName));

                return hand;
            }
        }

        public static IEnumerable<object[]> TestHands =>
          new List<object[]>
          {
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Ace, CardName.Jack }, new List<CardName> { CardName.Four, CardName.Five }, HandOutcome.Win) },
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Ace, CardName.Two }, new List<CardName> { CardName.Four, CardName.Ten }, HandOutcome.Lose)},
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Ace, CardName.Ten}, new List<CardName> { CardName.Ace, CardName.Queen}, HandOutcome.Draw)},
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Queen, CardName.Five}, new List<CardName> { CardName.Ace,CardName.Four, CardName.Ten }, HandOutcome.Draw)},
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Ace, CardName.Ace, CardName.Five}, new List<CardName> { CardName.Seven, CardName.King }, HandOutcome.Draw)},
                new object[] {new BlackJackTestHand(new List<CardName>() { CardName.Ace, CardName.Ace, CardName.Two}, new List<CardName> { CardName.Seven, CardName.King }, HandOutcome.Lose)},
       };

        [TestMethod]
        [DynamicData(nameof(TestHands), DynamicDataSourceType.Property)]
        public void TestBlackjackHandOutcomes(BlackJackTestHand testHand)
        {
            var standardDeck = new StandardDeck();
            var blackJackGame = new Blackjack();

            var playerHand = BlackJackTestHand.GetPlayerHand(testHand.PlayerHand, standardDeck);
            var oponentHand = BlackJackTestHand.GetPlayerHand(testHand.OpponentHand, standardDeck);
            
            Assert.AreEqual(testHand.HandOutcome,blackJackGame.GetHandOutcome(playerHand, oponentHand)  );

        }

        [TestMethod]
        public void DealGame()
        {
            var rounds = 50;
            var players = 5;

            foreach(var round in Enumerable.Range(1, rounds))
            {
                var blackjackGame = new Blackjack();
                blackjackGame.CreateGame(players);
                blackjackGame.DealHand();
                blackjackGame.OutputHandResults();
            }

        }

    }
}
