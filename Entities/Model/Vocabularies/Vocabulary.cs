namespace Entities.Model.Vocabularies
{
    public class Vocabulary : BaseModel<Guid>
    {
        public  int UserId { get; set; }
        public  string Word { get; set; }
        public  string Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }
        public int SeenCount { get; set; }
        public DateTime LastSeenDateTime { get; set; }
        public DateTime? LastEditDateTime { get; set; }
        public int BoxNumber { get; set; }
    }
}
