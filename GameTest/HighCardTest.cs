using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GameLib.Tables;
using System.Collections.Generic;
using GameLib.Games;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

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
            var rounds = 50;
            var players = 5;

            foreach(var round in Enumerable.Range(1, rounds))
            {
                var standardDeck = new StandardDeck();
                var highCardGame = new HighCard();

                highCardGame.SetDeck(standardDeck);
                highCardGame.Shuffle();

                var playerList = new List<Player>();
                foreach(var index in Enumerable.Range(0, players))
                {
                    playerList.Add(new Player() { Name = $"Player {index}" });
                }

                var playerHands = new List<Hand>();
                foreach(var player in playerList)
                {
                    highCardGame.AddPlayer(player);
                    playerHands.Add(highCardGame.AddPlayerHand(player));
                }

                foreach(var hand in playerHands)
                {
                    highCardGame.DealToPlayer(1, hand);
                    Assert.AreEqual(hand.Cards.Count, 1);
                }

                foreach(var hand in playerHands)
                {
                    foreach(var opponentHand in playerHands.Where( h => h!=hand))
                    {
                        var handOutcome = highCardGame.GetHandOutcome(hand, opponentHand);
                        Debug.WriteLine($"outcome round {round}: {hand.Player.Name} vs {opponentHand.Player.Name} card {hand.Cards.First().Name } { handOutcome} against {opponentHand.Cards.First().Name} ");
                    }
                }

            }


        }

    }
}
