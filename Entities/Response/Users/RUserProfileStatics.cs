using Entities.Enum.Friendships;

namespace Entities.Response.Users
{
    public class RUserProfileStatics : RUserPublicInfo
    {
        public int TotalVocabularyCount { get; set; }
        public List<int> InBoxVocabularyCount { get; set; } = new List<int>();
        public string RegisterAge { get; set; }
        public string LastCheck { get; set; }
        public string LastAddVocabulary { get; set; }
        public int TotalCheck { get; set; }
        public int AverageDailyCheck { get; set; }
        public int AverageDailyLearnedCheck { get; set; }
        public int AverageDailyNotLearnedCheck { get; set; }
        public int AverageDailyAdd { get; set; }
    }
}
