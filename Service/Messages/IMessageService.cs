using Entities.Form.Messages;

namespace Service.Messages
{
    public interface IMessageService
    {
        Task AddMessage(FAddMessage model);
    }
}
