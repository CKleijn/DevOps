using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class SlackAdapter : INotificationAdapter
    {
        public NotificationProvider Type { get; init; }
    
        public SlackAdapter()
        {
            Type = NotificationProvider.SLACK;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) {
            
            //send slack to all targeted users
            foreach(var recipient in notification.TargetUsers) {
                new Slack().SendSlack(notification.Body, recipient.Id.ToString(), notification.Sender.Id.ToString());
            }
        }
    }
}

