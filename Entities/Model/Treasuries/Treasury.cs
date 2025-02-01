using Entities.Model.MessageAttachments;
using Entities.Model.TreasuryLogs;

namespace Entities.Model.Treasuries
{
    public class Treasury : BaseModel<Guid>
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<TreasuryLog> TreasuryLogs { get; set; }
        public ICollection<TreasuryWord> TreasuryWords { get; set; }

    }
}
