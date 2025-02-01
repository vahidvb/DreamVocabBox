using Entities.Model.Messages;

namespace Entities.Model.Treasuries
{
    public class TreasuryWord : BaseModel<Guid>
    {
        public Guid TreasuryId { get; set; }
        public string Word { get; set; } = "";
        public string? Meaning { get; set; }
        public string? Example { get; set; }
        public string? Description { get; set; }

        public virtual Treasury Treasury { get; set; }
    }
}
