using Microsoft.EntityFrameworkCore;

namespace Entities.Model.Dictionaries
{
    public class DictionaryBase
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Definition { get; set; }
    }
}
