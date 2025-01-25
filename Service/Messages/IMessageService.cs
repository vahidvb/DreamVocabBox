﻿using Entities.Form.MessageAttachments;
using Entities.Form.Messages;
using Entities.Response.Messages;

namespace Service.Messages
{
    public interface IMessageService
    {
        Task AddMessage(FAddMessage model);
        Task<RMessagesListPagination> GetMessagesListAsync(FGetMessagePagination form);
    }
}
