using Data;
using Microsoft.Extensions.Configuration;

namespace Business
{
    public class BaseBusiness
    {
        protected readonly DreamVocabBoxContext DataBase;
        protected readonly IConfiguration Configuration;
        public BaseBusiness(DreamVocabBoxContext db, IConfiguration configuration)
        {
            this.DataBase = db;
            this.Configuration = configuration;
        }
    }
}
