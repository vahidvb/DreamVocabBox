using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Vocabularies
{
    public class RVocabularyBox
    {
        public int BoxNumber { get; set; }
        public int AllCount { get; set; }
        public int UnCheckedCount { get; set; }
        public int CheckedCount { get; set; }
        public string SoonTime { get; set; }
    }
}
