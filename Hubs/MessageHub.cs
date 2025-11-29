using EphemeralRealTimeChatApp.Models;
using EphemeralRealTimeChatApp.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace EphemeralRealTimeChatApp.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Message>> LoadMessages()
        {
            List<Message> messages = await _unitOfWork.Messages.GetAllMessagesAsync();
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
            await _unitOfWork.Messages.AddNewMessageAsync(message);

            await Clients.All.SendAsync("ReceiveMessage",
                message.UserEmail,
                message.Text,
                message.SentAtUtc.ToString("yyyy-MM-dd, HH:mm:ss")
            );
        }
    }
}
