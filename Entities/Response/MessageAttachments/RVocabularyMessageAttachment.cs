using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.MessageAttachments
{
    public class RVocabularyMessageAttachment
    {
        public string Word { get; set; }
        public string? Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }
    }
}
