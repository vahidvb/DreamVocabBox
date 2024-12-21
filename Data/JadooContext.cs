using Entities.Model.Artists;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JadooContext : BaseContext
    {
        public JadooContext(DbContextOptions<JadooContext> options) : base(options)
        {
        }
        public DbSet<Artist> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
