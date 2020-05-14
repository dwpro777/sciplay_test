using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GameLib.Tables;
using System.Collections.Generic;
using GameLib.Games;

namespace GameTest
{
    [TestClass]
    public class HighCardTest
    {

        public class HighCardTestHand
        {
            public List<CardName> PlayerHand { get; set; }
            public List<CardName> OpponentHand { get; set; }
            public HandOutcome HandOutcome { get; set; }
            public HighCardTestHand(List<CardName> playerHand, List<CardName> opponentHand, HandOutcome handOutcome)
            {
                PlayerHand = playerHand;
                OpponentHand = opponentHand;
                HandOutcome = handOutcome;
            }

            public static Hand GetPlayerHand(List<CardName> cardNames, StandardDeck deck)
            {
                var hand = new Hand();
                foreach (var cardName in cardNames)
                    hand.Cards.Add(deck.Cards.First(c => c.Name == cardName));

                return hand;
            }
        }

        public static IEnumerable<object[]> TestHands =>
          new List<object[]>
          {
                new object[] {new HighCardTestHand(new List<CardName>() { CardName.Ace}, new List<CardName> { CardName.Jack}, HandOutcome.Win) },
                new object[] {new HighCardTestHand(new List<CardName>() { CardName.Four}, new List<CardName> { CardName.Jack}, HandOutcome.Lose) },
                new object[] {new HighCardTestHand(new List<CardName>() { CardName.Two}, new List<CardName> { CardName.Two}, HandOutcome.Draw) },
       };

        [TestMethod]
        [DynamicData(nameof(TestHands), DynamicDataSourceType.Property)]
        public void TestHighCardHandOutcomes(HighCardTestHand testHand)
        {
            var standardDeck = new StandardDeck();
            var highCardGame = new HighCard();

            var playerHand = HighCardTestHand.GetPlayerHand(testHand.PlayerHand, standardDeck);
            var oponentHand = HighCardTestHand.GetPlayerHand(testHand.OpponentHand, standardDeck);
            Assert.AreEqual(testHand.HandOutcome,highCardGame.GetHandOutcome(playerHand, oponentHand)  );

        }

        [TestMethod]
        public void DealGame()
        {
            var standardDeck = new StandardDeck();
            var highCardGame = new HighCard();

            highCardGame.setDeck(standardDeck);
            highCardGame.Shuffle();

            var player1 = new Player() { Name = "player1" };
            var player2 = new Player() { Name = "player2" };

            highCardGame.AddPlayer(player1);
            highCardGame.AddPlayer(player2);

            var player1Hand = highCardGame.AddPlayerHand(player1);
            var player2Hand = highCardGame.AddPlayerHand(player2);

            highCardGame.DealToPlayer(1, player1Hand);
            highCardGame.DealToPlayer(1, player2Hand);


            Assert.AreEqual(player1Hand.Cards.Count, 1);
            Assert.AreEqual(player2Hand.Cards.Count, 1);

            var handOutcome = highCardGame.GetHandOutcome(player1Hand, player2Hand);

        }

    }
}
