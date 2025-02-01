using Common.Api;
using Entities.Form.Treasuries;
using Entities.Model.Treasuries;
using Entities.Response.Treasuries;

namespace Service.Treasuries
{
    public interface ITreasuryService
    {
        Task<string> Create(FTreasuryCreate form);
        Task<Treasury> GetById(string TreasuryId, int UserId);
        Task<RTreasuryPagination> GetAll(FGetTreasuryPagination form);
    }
}
