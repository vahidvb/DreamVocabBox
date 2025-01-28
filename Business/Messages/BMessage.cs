using Common.Extensions;
using Data;
using Entities.Form.MessageAttachments;
using Entities.Form.Messages;
using Entities.Model.MessageAttachments;
using Entities.Model.Messages;
using Entities.Model.Users;
using Entities.Model.Vocabularies;
using Entities.Response.Messages;
using Entities.Response.Vocabularies;
using Microsoft.EntityFrameworkCore;
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

            DataBase.Messages.Add(message);
            await DataBase.SaveChangesAsync();

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
                    DataBase.MessageAttachments.Add(messageAttachment);
                }
                await DataBase.SaveChangesAsync();
            }
        }
        public async Task<RVocabularyMessagePagination> GetMessages(FGetMessagePagination form)
        {
            var messages = await DataBase.Messages
                .Where(m => (m.ReceiverUserId == form.UserId || m.SenderUserId == form.UserId) && (m.ReceiverUserId == form.FriendUserId || m.SenderUserId == form.FriendUserId))
                .OrderByDescending(x => x.RegisterDate)
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .ToListAsync();

            var totalMessages = await DataBase.Messages.AsNoTracking()
                    .Where(m => (m.ReceiverUserId == form.UserId || m.SenderUserId == form.UserId) && (m.ReceiverUserId == form.FriendUserId || m.SenderUserId == form.FriendUserId))
                     .CountAsync();

            var result = new List<RMessage>();
            foreach (var message in messages)
            {
                var temp = new RMessage()
                {
                    IsUserSent = message.SenderUserId == form.UserId,
                    RegisterDate = message.RegisterDate,
                    RegisterDateHumanReadable = message.RegisterDate.ToAutoHumanReadableTime(),
                    Content = message.Content,
                    Reaction = message.Reaction,
                    ReadAt = message.ReadAt != null ? message.ReadAt.ToNotNullable().ToAutoHumanReadableTime() : null,
                    ReceiverUserId = message.ReceiverUserId,
                    SenderUserId = message.SenderUserId,
                };
                if (!temp.IsUserSent && message.ReadAt == null)
                {
                    message.ReadAt = DateTime.Now;
                    DataBase.Messages.Update(message);
                    await DataBase.SaveChangesAsync();
                    temp.ReadAt = message.ReadAt != null ? message.ReadAt.ToNotNullable().ToAutoHumanReadableTime() : null;
                }
                message.MessageAttachments = await DataBase.MessageAttachments.Where(x => x.MessageId == message.Id).ToListAsync();
                foreach (var attachment in message.MessageAttachments)
                {
                    switch (attachment.Type)
                    {
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.Vocabulary:
                            var vocabulary = DataBase.Vocabularies.FirstOrDefault(x => x.Word.ToLower().Trim() == attachment.Value && x.UserId == message.SenderUserId);
                            if (vocabulary != null)
                                temp.Attachments.Add(new RVocabulary() { Description = vocabulary.Description, Example = vocabulary.Example, Meaning = vocabulary.Meaning, Word = vocabulary.Word });
                            else
                                temp.Attachments.Add(new RVocabulary() { Word = attachment.Value });
                            break;
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.Box:
                            break;
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.File:
                            break;
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.Image:
                            break;
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.Audio:
                            break;
                        case Entities.Enum.MessageAttachments.MessageAttachmentTypeEnum.Video:
                            break;
                        default:
                            break;
                    }
                }
                result.Add(temp);
            }
            return new RVocabularyMessagePagination()
            {
                Items = result,
                CurrentPage = form.ListPosition / form.ListLength + 1,
                TotalPage = (int)Math.Ceiling((double)totalMessages / form.ListLength),
                PageSize = form.ListLength,
                TotalItem = totalMessages
            };
        }
        public async Task<RMessagesListPagination> GetMessagesList(FGetMessagePagination form)
        {
            var messages = await DataBase.Messages
                .Where(m => m.ReceiverUserId == form.UserId || m.SenderUserId == form.UserId)
                .GroupBy(m => new
                {
                    User1 = m.SenderUserId < m.ReceiverUserId ? m.SenderUserId : m.ReceiverUserId,
                    User2 = m.SenderUserId < m.ReceiverUserId ? m.ReceiverUserId : m.SenderUserId
                })
                .Select(g => g.OrderByDescending(m => m.RegisterDate).FirstOrDefault())
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .ToListAsync();

            var result = new List<RMessagesList>();
            foreach (var x in messages)
            {
                var UserId = x.SenderUserId == form.UserId ? x.ReceiverUserId : x.SenderUserId;
                var UserInRepository = userRepositoryService.Get(UserId);
                var messageAttachments = await DataBase.MessageAttachments.Where(xx => xx.MessageId == x.Id).Select(xx => xx.Value ?? "").ToListAsync();
                if (UserInRepository != null)
                    result.Add(new RMessagesList()
                    {
                        Avatar = UserInRepository.Avatar,
                        LastMessage = x.Content + (messageAttachments.Count() > 0 ? x.Content != "" ? " | " : "" + messageAttachments.ToCommaSeperated() : ""),
                        NickName = UserInRepository.NickName,
                        UserId = UserId,
                        UserName = UserInRepository.UserName,
                        UnreadCount = await DataBase.Messages.AsNoTracking().CountAsync(xx => xx.ReadAt == null && (xx.ReceiverUserId == form.UserId && xx.SenderUserId == UserId)),
                    });
                else
                {
                    var user = await DataBase.Users.FirstOrDefaultAsync(xx => xx.Id == UserId);
                    result.Add(new RMessagesList()
                    {
                        Avatar = user.Avatar,
                        LastMessage = x.Content + (messageAttachments.Count() > 0 ? x.Content != "" ? " | " : "" + messageAttachments.ToCommaSeperated() : ""),
                        NickName = user.NickName,
                        UserId = UserId,
                        UserName = user.UserName,
                        UnreadCount = await DataBase.Messages.AsNoTracking().CountAsync(xx => xx.ReadAt == null && (xx.ReceiverUserId == form.UserId && xx.SenderUserId == UserId)),
                    });
                }
            }

            var totalMessages = await DataBase.Messages.AsNoTracking()
                .Where(m => m.ReceiverUserId == form.UserId || m.SenderUserId == form.UserId)
                 .GroupBy(m => new { m.SenderUserId, m.ReceiverUserId }).CountAsync();



            return new RMessagesListPagination
            {
                Items = result,
                CurrentPage = form.ListPosition / form.ListLength + 1,
                TotalPage = (int)Math.Ceiling((double)totalMessages / form.ListLength),
                PageSize = form.ListLength,
                TotalItem = totalMessages
            };
        }

    }
}
