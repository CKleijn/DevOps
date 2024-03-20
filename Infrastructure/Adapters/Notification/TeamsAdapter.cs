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
            
            //send teams message to currently targeted user
            new Teams().SendTeamsMessage(notification.Body, notification.CurrentTargetUser!.Id.ToString());
        }
    }
}