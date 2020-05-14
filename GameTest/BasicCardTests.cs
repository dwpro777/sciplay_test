using GameLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GameLib.Tables;

namespace GameTest
{
    [TestClass]
    public class BasicCardTests
    {

        [TestMethod]
        public void TestStandardDeckSize()
        {
            var expectedDeckSize = 52;
            var expectedSuitNumber = 4;
            var deck = new StandardDeck();

            Assert.IsTrue(deck.Cards.Count == expectedDeckSize);
            Assert.IsTrue(deck.Cards.Where(card => card.Suit == CardSuit.Club).Count() == expectedDeckSize / expectedSuitNumber);

        }

    }
}
