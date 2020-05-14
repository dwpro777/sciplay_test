using GameLib.Tables;
using Microsoft.EntityFrameworkCore;

namespace GameLib
{
    public class gameDbContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameCard> GameDecks { get; set; }
        public DbSet<Card> Cards { get; set; }

    }
}
