using Business.Users;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Users;


public class TestFixture
{
    public DreamVocabBoxContext DataBase { get; private set; }
    public IConfiguration Configuration { get; private set; }
    public IUserRepositoryService UserRepositoryService { get; private set; }

    public TestFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<DreamVocabBoxContext>()
        .UseSqlServer(Configuration.GetConnectionString("DbCS"))
         .Options;

        DataBase = new DreamVocabBoxContext(options);

        UserRepositoryService = new BUserRepository(DataBase, Configuration);
    }
}