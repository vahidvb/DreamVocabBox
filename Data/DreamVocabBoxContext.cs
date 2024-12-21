using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DreamVocabBoxContext : BaseContext
    {
        public DreamVocabBoxContext(DbContextOptions<DreamVocabBoxContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
