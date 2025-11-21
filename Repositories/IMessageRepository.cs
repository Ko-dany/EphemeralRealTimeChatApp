using EphemeralRealTimeChatApp.Models;

namespace EphemeralRealTimeChatApp.Repositories
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetAllMessagesAsync();
        public Task AddNewMessageAsync(Message message);
        public Task DeleteOldMessagesAsync(DateTime cutoffTime);
    }
}
