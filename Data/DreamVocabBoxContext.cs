using Entities.Model.Dictionaries;
using Entities.Model.Users;
using Entities.Model.Vocabularies;
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

        #region Add Dictionary Tables ****** These tables will be filled in OnModelCreating ******
        public DbSet<DictionaryEnglishToPersian> DictionaryEnglishToPersians { get; set; }
        public DbSet<DictionaryPersianToEnglish> DictionaryPersianToEnglishs { get; set; }
        public DbSet<DictionaryEnglishToEnglish> DictionaryEnglishToEnglishs { get; set; }
        public DbSet<IdiomsEnglishToPersian> IdiomsEnglishToPersians { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
