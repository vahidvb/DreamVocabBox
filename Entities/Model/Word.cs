using Entities.Enum.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Word : BaseModel<Guid>
    {
        public required int UserId { get; set; }
        public required string Value { get; set; }
        public required string Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }
        public int SeenCount { get; set; }
        public DateTime LastSeenDateTime { get; set; }
    }
}
