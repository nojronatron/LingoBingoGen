using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LingoBingoLibrary.DataAccess
{
    public class LingoWordsContext : DbContext
    {
        public LingoWordsContext(DbContextOptions options): base(options) 
        { // can be empty to start
        }
        public DbSet<LingoWord> LingoWords { get; set; }
        public DbSet<LingoCategory> LingoCategories { get; set; }
    }
}
