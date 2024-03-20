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
            
            //send slack to currently targeted user
            new Slack().SendSlack(notification.Body, notification.CurrentTargetUser!.Id.ToString());
        }
    }
}

