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
                var standardDeck = new StandardDeck();
                var blackjackGame = new Blackjack();

                blackjackGame.SetDeck(standardDeck);
                blackjackGame.Shuffle();

                var playerList = new List<Player>();
                foreach(var index in Enumerable.Range(0, players))
                {
                    playerList.Add(new Player() { Name = $"Player {index}" });
                }

                var playerHands = new List<Hand>();
                foreach(var player in playerList)
                {
                    blackjackGame.AddPlayer(player);
                    playerHands.Add(blackjackGame.AddPlayerHand(player));
                }

                //give each player two cards
                foreach(var index in Enumerable.Range(0, 2))
                {
                    foreach(var hand in playerHands)
                    {
                        blackjackGame.DealToPlayer(1, hand);
                    }
                }

                foreach(var hand in playerHands)
                {
                    foreach(var opponentHand in playerHands.Where( h => h!=hand))
                    {
                        var handOutcome = blackjackGame.GetHandOutcome(hand, opponentHand);
                        Debug.WriteLine($"outcome round {round}: {hand.Player.Name} vs {opponentHand.Player.Name} card {hand.Cards[0].Name }/{hand.Cards[1].Name}  { handOutcome} against {opponentHand.Cards[0].Name}/{opponentHand.Cards[1].Name}  ");
                    }
                }

            }

        }

    }
}
