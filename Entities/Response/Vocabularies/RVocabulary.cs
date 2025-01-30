namespace Entities.Response.Vocabularies
{
    public class RVocabulary
    {
        public string Word { get; set; } = "";
        public string Meaning { get; set; } = "";
        public string Example { get; set; } = "";
        public string Description { get; set; } = "";
        public bool AllreadyAdded { get; set; } = false;
    }
}
