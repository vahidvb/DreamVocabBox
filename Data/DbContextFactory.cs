using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DreamVocabBoxContext>
    {
        public DreamVocabBoxContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();

            var builder = new DbContextOptionsBuilder<DreamVocabBoxContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DbCS") ?? "");
            return new DreamVocabBoxContext(builder.Options);
        }
    }
}
