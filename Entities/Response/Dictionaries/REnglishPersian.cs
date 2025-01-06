using Entities.Model.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Dictionaries
{
    public class REnglishPersian
    {
        public string Word { get; set; } = "";
        public string? DefinitionEn { get; set; }
        public string? DefinitionFa { get; set; }
        public string? Forms { get; set; }
    }
}
