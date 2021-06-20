using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LingoBingoLibrary.DataAccess
{
    public class LingoWordsContext : DbContext
    {
        public IConfiguration CustomConfig { get; set; }
        public LingoWordsContext(DbContextOptionsBuilder options)
        { // can be empty to start
            options.UseSqlServer("Data Source=.;Initial Catalog=LingoWebDb;Integrated Security=True");
        }
        public LingoWordsContext(DbContextOptions<LingoWordsContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public DbSet<LingoWord> LingoWords { get; set; }
        public DbSet<LingoCategory> LingoCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LingoWord>()
                .HasOne(e => e.LingoCategory);
            modelBuilder.Entity<LingoCategory>()
                .HasMany(e => e.LingoWords);
        }
    }

}
