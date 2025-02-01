using Entities.Response.Users;

namespace Entities.Response.Treasuries
{
    public class RTreasury
    {
        public RUserPublicInfo User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SeenCount { get; set; }
        public int AcquireCount { get; set; }
    }
    public class RTreasuryPagination : Pagination<RTreasury>
    {
    }
}
