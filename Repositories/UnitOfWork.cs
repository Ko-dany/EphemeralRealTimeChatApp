using EphemeralRealTimeChatApp.Data;

namespace EphemeralRealTimeChatApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IMessageRepository Messages { get; set; }

        public UnitOfWork(AppDbContext context, IMessageRepository Messages)
        {
            _context = context;
            this.Messages = Messages;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
