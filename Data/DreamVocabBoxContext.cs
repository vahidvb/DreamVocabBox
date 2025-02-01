using Entities.Model.Dictionaries;
using Entities.Model.Friendships;
using Entities.Model.MessageAttachments;
using Entities.Model.Messages;
using Entities.Model.Treasuries;
using Entities.Model.TreasuryLogs;
using Entities.Model.Users;
using Entities.Model.Vocabularies;
using Entities.Model.VocabularyChecks;
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
        public DbSet<VocabularyCheck> VocabularyChecks { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<Treasury> Treasuries { get; set; }
        public DbSet<TreasuryWord> TreasuryWords { get; set; }
        public DbSet<TreasuryLog> TreasuryLogs { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                    .HasMany(m => m.MessageAttachments)
                    .WithOne(a => a.Message)
                    .HasForeignKey(a => a.MessageId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Treasury>()
                    .HasMany(m => m.TreasuryLogs)
                    .WithOne(a => a.Treasury)
                    .HasForeignKey(a => a.TreasuryId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Treasury>()
                    .HasMany(m => m.TreasuryWords)
                    .WithOne(a => a.Treasury)
                    .HasForeignKey(a => a.TreasuryId)
                    .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);


        }
    }
}
