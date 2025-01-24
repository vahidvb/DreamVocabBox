using Data;
using Entities.Form.MessageAttachments;
using Entities.Form.Messages;
using Entities.Model.MessageAttachments;
using Entities.Model.Messages;
using Entities.Response.Messages;
using Microsoft.Extensions.Configuration;
using Service.Messages;
using Service.Users;
namespace Business.Messages
{
    public class BMessage(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : BaseBusiness(db, configuration, userRepositoryService), IMessageService
    {
        public async Task AddMessage(FAddMessage model)
        {
            var message = new Message
            {
                SenderUserId = model.SenderUserId,
                ReceiverUserId = model.ReceiverUserId,
                Content = model.Content,
            };

            db.Messages.Add(message);
            await db.SaveChangesAsync();

            if (model.Attachments != null && model.Attachments.Count > 0 && message.Id != Guid.Empty)
            {
                foreach (var attachment in model.Attachments)
                {
                    var messageAttachment = new MessageAttachment
                    {
                        MessageId = message.Id,
                        Value = attachment.Value,
                        Type = attachment.Type,
                    };
                    db.MessageAttachments.Add(messageAttachment);
                }
                await db.SaveChangesAsync();
            }
        }
        public async Task<RVocabularyMessagePagination<dynamic>> GetMessageAsync(FGetMessagePagination form)
        {
            return null;
        }

    }
}
