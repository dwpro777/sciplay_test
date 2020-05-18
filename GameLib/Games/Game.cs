using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Games
{
    public abstract class Game:Tables.Game
    {

        public List<Player> Players { get; private set; }

        public Game()
        {
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public virtual void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public virtual void CreateGame(int playerCount)
        {
            foreach(var index in Enumerable.Range(0, playerCount))
            {
                AddPlayer(new Player() { Name = $"Player {index}" });
            }

        }

        public abstract GameType GetGameType();

    }
}
