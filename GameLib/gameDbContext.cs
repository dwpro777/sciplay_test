using GameLib.Tables;
using Microsoft.EntityFrameworkCore;

namespace GameLib
{
    public class GameDbContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameCard> GameCards { get; set; }
        public DbSet<Card> Cards { get; set; }

    }
}
