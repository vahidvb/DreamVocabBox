using Entities.Enum.TreasuryLogs;
using Entities.Model.Treasuries;

namespace Entities.Model.TreasuryLogs
{
    public class TreasuryLog : BaseModel<Guid>
    {
        public int UserId { get; set; }
        public Guid TreasuryId { get; set; }
        public TreasuryLogTypeEnum Type { get; set; }

        public virtual Treasury Treasury { get; set; }
    }
}
