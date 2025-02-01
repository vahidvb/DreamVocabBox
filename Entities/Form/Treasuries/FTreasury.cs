using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Form.Treasuries
{
    public class FTreasuryCreate
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class FGetTreasuryPagination
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public int ListLength { get; set; }
        public int ListPosition { get; set; }
        public string? SearchText { get; set; }
    }
}
