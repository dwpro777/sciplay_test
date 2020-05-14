using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib.Tables
{
    public class Hand 
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public List<Card> Cards{ get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }
    }
}
