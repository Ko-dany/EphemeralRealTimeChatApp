using EphemeralRealTimeChatApp.Hubs;
using EphemeralRealTimeChatApp.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace EphemeralRealTimeChatApp.Services
{
    public class MessageCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageCleanupService(IServiceProvider serviceProvider, IHubContext<MessageHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
                    var cutoffDate = DateTime.UtcNow.AddMinutes(-0.5);
                    var deletedCount = await repository.DeleteOldMessagesAsync(cutoffDate);

                    if(deletedCount > 0)
                    {
                        await _hubContext.Clients.All.SendAsync("MessagesCleanedUp", stoppingToken);
                    }

                }
                await Task.Delay(TimeSpan.FromMinutes(0.1), stoppingToken);
            }
        }
    }
}
