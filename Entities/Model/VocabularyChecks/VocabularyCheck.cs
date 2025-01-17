namespace Entities.Model.VocabularyChecks
{
    public class VocabularyCheck : BaseModel<Guid>
    {
        public int UserId { get; set; }
        public bool Learned { get; set; }
        public Guid VocabularyId { get; set; }
        public int BoxNumber { get; set; }
    }
}
