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
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }
    }
    public class FRemoveVocabulary
    {
        public required Guid VocabularyId { get; set; }
        public required int UserId { get; set; }
    }
}
