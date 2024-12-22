using Business.Vocabularies;
using Service.Vocabularies;

namespace Service
{
    public static class AddServices
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IVocabularyService, BVocabulary>();
        }

    }
}
