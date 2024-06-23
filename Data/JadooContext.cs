using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JadooContext : BaseContext
    {
        public JadooContext(DbContextOptions<JadooContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
