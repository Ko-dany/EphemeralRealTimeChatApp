namespace EphemeralRealTimeChatApp.Repositories
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages { get; }

        int Complete();
    }
}
