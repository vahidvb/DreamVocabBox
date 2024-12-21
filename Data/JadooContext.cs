using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JadooContext : BaseContext
    {
        public JadooContext(DbContextOptions<JadooContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
