using EphemeralRealTimeChatApp.Models;
using EphemeralRealTimeChatApp.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace EphemeralRealTimeChatApp.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _repository;

        public MessageHub(IMessageRepository messageRepository)
        {
            _repository = messageRepository;
        }

        public async Task<List<Message>> LoadMessages()
        {
            List<Message> messages = await _repository.GetAllMessagesAsync();
            return messages;
        }

        public async Task SendMessage(string userEmail, string messageText)
        {
            Message message = new Message
            {
                UserEmail = userEmail,
                Text = messageText,
                SentAtUtc = DateTime.UtcNow
            };
            await _repository.AddNewMessageAsync(message);

            await Clients.All.SendAsync("ReceiveMessage",
                message.UserEmail,
                message.Text,
                message.SentAtUtc.ToString("HH:mm:ss")
            );
        }
    }
}
