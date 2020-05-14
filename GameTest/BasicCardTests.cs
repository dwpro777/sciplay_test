using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using GameLib.Tables;
using Xunit;
using System.Collections.Generic;
using GameLib.Games;

namespace GameTest
{
    [TestClass]
    public class BasicCardTests
    {

        [TestMethod]
        public void testStandardDeckSize()
        {
            var expectedDeckSize = 52;
            var expectedSuitNumber = 4;
            var deck = new StandardDeck();

            Assert.IsTrue(deck.Cards.Count == expectedDeckSize);
            Assert.IsTrue(deck.Cards.Where(card => card.Suit == CardSuit.Club).Count() == expectedDeckSize / expectedSuitNumber);

        }



    }
}
