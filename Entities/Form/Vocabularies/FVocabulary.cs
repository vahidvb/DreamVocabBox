using System.Text.Json.Serialization;

namespace Entities.Form.Vocabularies
{
    public class GetVocabularyPaginationForm
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int ListLength { get; set; }
        public int ListPosition { get; set; }
    }
}
