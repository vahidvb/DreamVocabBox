using Business;
using Business.Users;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Service.Users;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;


public class BaseTest<TBusiness> where TBusiness : BaseBusiness
{
    protected TBusiness Business { get; private set; }
    protected DreamVocabBoxContext DataBase { get; private set; }
    protected IConfiguration Configuration { get; private set; }
    protected IUserRepositoryService UserRepositoryService { get; private set; }

    public BaseTest()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<DreamVocabBoxContext>()
            .UseSqlServer(Configuration.GetConnectionString("DbCS"))
            .Options;

        DataBase = new DreamVocabBoxContext(options);

        UserRepositoryService = new BUserRepository(DataBase, Configuration);

        Type myClassType = typeof(TBusiness);

        ConstructorInfo[] constructors = myClassType.GetConstructors();

        foreach (var constructor in constructors)
        {
            if (constructor.GetParameters().Length == 2)
            {
                Business = (TBusiness?)Activator.CreateInstance(typeof(TBusiness), DataBase, Configuration) ?? throw new InvalidOperationException("Failed to create instance of TBusiness.");
                break;
            }
            if (constructor.GetParameters().Length == 3)
            {
                Business = (TBusiness?)Activator.CreateInstance(typeof(TBusiness), DataBase, Configuration, UserRepositoryService) ?? throw new InvalidOperationException("Failed to create instance of TBusiness.");
                break;
            }
        }
    }
}
