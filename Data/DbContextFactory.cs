using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<JadooContext>
    {
        public JadooContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<JadooContext>();

            builder.UseSqlServer(configuration.GetConnectionString("DbCS") ?? "");

            return new JadooContext(builder.Options);
        }
    }
}
