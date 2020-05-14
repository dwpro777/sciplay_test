using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib.Tables
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameType GameType { get; set; }
        public int GameCardId { get; set; } 
        public virtual GameCard GameDeck { get; set; }
    }
}
