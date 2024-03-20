using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class TeamsAdapter : INotificationAdapter
    {
        private NotificationProvider _type { get; init; }
        public NotificationProvider Type { get => _type; init => _type = value; }

        public TeamsAdapter()
        {
            Type = NotificationProvider.TEAMS;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) 
        {
            new Teams().SendTeamsMessage(notification.Body, notification.Recipient!.Id.ToString());
        }
    }
}