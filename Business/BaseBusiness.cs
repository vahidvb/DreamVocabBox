using Data;
using Microsoft.Extensions.Configuration;
using Service.Users;

namespace Business
{
    public class BaseBusiness
    {
        protected readonly DreamVocabBoxContext DataBase;
        protected readonly IConfiguration Configuration;
        protected readonly IUserRepositoryService userRepositoryService;
        public BaseBusiness(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService)
        {
            this.DataBase = db;
            this.Configuration = configuration;
            this.userRepositoryService = userRepositoryService;
        }
        public BaseBusiness(DreamVocabBoxContext db, IConfiguration configuration)
        {
            this.DataBase = db;
            this.Configuration = configuration;
        }
    }
}
