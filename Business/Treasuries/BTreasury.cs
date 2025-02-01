using Common;
using Common.Extensions;
using Data;
using Entities.Form.Treasuries;
using Entities.Model.Treasuries;
using Entities.Response.Treasuries;
using Entities.Response.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Treasuries;
using Service.Users;

namespace Business.Treasuries
{
    public class BTreasury(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : BaseBusiness(db, configuration, userRepositoryService), ITreasuryService
    {
        public async Task<string> Create(FTreasuryCreate form)
        {
            if (string.IsNullOrEmpty(form.Name.Trim()))
                throw new AppException(Common.Api.ApiResultStatusCode.RequiedFields);

            var add = new Treasury()
            {
                UserId = form.UserId,
                Description = form.Description,
                Name = form.Name,
            };

            DataBase.Treasuries.Add(add);
            await DataBase.SaveChangesAsync();

            if (add.Id != Guid.Empty)
                return add.Id.ToString();
            else
                throw new AppException(Common.Api.ApiResultStatusCode.BadRequest);
        }

        public async Task<RTreasuryPagination> GetAll(FGetTreasuryPagination form)
        {
            var query = DataBase.Treasuries.AsQueryable();
            query = query.Where(t => (form.UserId == null || t.UserId == form.UserId) && (string.IsNullOrEmpty(form.SearchText) || (t.Name.StartsWith(form.SearchText, StringComparison.CurrentCultureIgnoreCase) || (t.Description != null && t.Description.StartsWith(form.SearchText, StringComparison.CurrentCultureIgnoreCase)))));
            var totalItems = await query.CountAsync();
            var items = await query
                .Where(t => form.UserId == null || t.UserId == form.UserId)
                .OrderBy(t => t.TreasuryLogs.Count())
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .Select(r => new RTreasury()
                {
                    Description = r.Description ?? "",
                    Name = r.Name,
                    User = userRepositoryService.GetIfNotExistDatabase(r.UserId).MapTo<RUserPublicInfo>(),
                    AcquireCount = r.TreasuryLogs.Count(x => x.Type == Entities.Enum.TreasuryLogs.TreasuryLogTypeEnum.Acquire),
                    SeenCount = r.TreasuryLogs.Count(x => x.Type == Entities.Enum.TreasuryLogs.TreasuryLogTypeEnum.Show)
                })
                .ToListAsync();

            return new RTreasuryPagination
            {
                Items = items,
                CurrentPage = form.ListPosition / form.ListLength + 1,
                TotalPage = (int)Math.Ceiling(totalItems / (double)form.ListLength),
                PageSize = form.ListLength,
                TotalItem = totalItems
            };
        }

        public async Task<Treasury> GetById(string TreasuryId, int UserId) => await DataBase.Treasuries.FirstOrDefaultAsync(t => t.Id == TreasuryId.ToGuid() && t.UserId == UserId) ?? throw new AppException(Common.Api.ApiResultStatusCode.NotFound);
    }
}
