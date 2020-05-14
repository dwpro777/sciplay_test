using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib.Tables
{
    public class GameCard
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
