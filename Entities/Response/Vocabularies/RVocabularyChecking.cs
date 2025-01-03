using Entities.Model.Vocabularies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Vocabularies
{
    public class RVocabularyChecking : Vocabulary
    {
        public bool Locked { get; set; }
    }
}
