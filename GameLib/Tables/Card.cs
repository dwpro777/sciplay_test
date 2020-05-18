using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GameLib.Tables
{
    public class Card
    {
        public int Id { get; set; }
        public CardName Name { get; set; }
        public CardSuit Suit { get; set; }
        public int[] Values { get; set; }
        public string fileName{ get; set; }
        public bool IsVisible{ get; set; }

    }
}
