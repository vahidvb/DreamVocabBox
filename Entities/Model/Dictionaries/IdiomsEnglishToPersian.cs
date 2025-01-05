using Microsoft.EntityFrameworkCore;

namespace Entities.Model.Dictionaries
{
    public class IdiomsEnglishToPersian
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
        public string Base { get; set; }
        public string Definition { get; set; }
    }
}
