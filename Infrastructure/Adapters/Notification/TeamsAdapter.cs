using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class TeamsAdapter : INotificationAdapter
    {
        public NotificationProvider Type { get; init; }
    
        public TeamsAdapter()
        {
            Type = NotificationProvider.TEAMS;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) {
            
            //send slack to all targeted users
            foreach(var recipient in notification.TargetUsers) {
                new Teams().SendTeamsMessage(notification.Body, recipient.Id.ToString(), notification.Sender.Id.ToString());
            }
        }
    }
}