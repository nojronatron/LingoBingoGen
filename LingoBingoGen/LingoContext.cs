using System.Data.Entity;

namespace LingoBingoGen
{
    public class LingoContext : DbContext
    {
        public LingoContext() : base("LingoDB")
        {
            Database.SetInitializer<LingoContext>(new LingoDbInitializer());
        }
        public DbSet<LingoWord> LingoWords { get; set; }
    }
}
