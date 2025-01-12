using Business;
using Business.Dictionaries;
using Business.Users;
using Business.Vocabularies;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Service.Users;
using Service.Vocabularies;
using System.Text;

namespace Service
{
    public static class AddServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IVocabularyService, BVocabulary>();
            services.AddScoped<IUserService, BUser>();
            services.AddScoped<IDictionaryService, BDictionary>();
            services.AddScoped<IUserRepositoryService, BUserRepository>();
        }

    }
}
