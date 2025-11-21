using EphemeralRealTimeChatApp.Data;
using EphemeralRealTimeChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EphemeralRealTimeChatApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return _context.Messages.OrderBy(m => m.SentAtUtc).ToList();
        }

        public async Task AddNewMessageAsync(Message message)
        {
            _context.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteOldMessagesAsync(DateTime cutoffTime)
        {
            var oldMessages = await _context.Messages.Where(m => m.SentAtUtc < cutoffTime).ToListAsync();
            _context.Messages.RemoveRange(oldMessages);
            await _context.SaveChangesAsync();
            return oldMessages.Count;
        }

    }
}
