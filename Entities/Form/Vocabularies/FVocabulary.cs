using System.Text.Json.Serialization;

namespace Entities.Form.Vocabularies
{
    public class FGetVocabularyPagination
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int ListLength { get; set; }
        public int ListPosition { get; set; }
    }
    public class FAddEditVocabulary
    {
        public string? Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }
    }
    public class FRemoveVocabulary
    {
        public required string VocabularyId { get; set; }
        [JsonIgnore]
        public required int UserId { get; set; }
    }
}
