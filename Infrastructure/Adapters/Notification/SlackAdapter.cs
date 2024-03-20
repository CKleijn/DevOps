using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class SlackAdapter : INotificationAdapter
    {
        private NotificationProvider _type { get; init; }
        public NotificationProvider Type { get => _type; init => _type = value; }

        public SlackAdapter()
        {
            Type = NotificationProvider.SLACK;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) 
        {
            new Slack().SendSlack(notification.Body, notification.Recipient!.Id.ToString());
        }
    }
}

