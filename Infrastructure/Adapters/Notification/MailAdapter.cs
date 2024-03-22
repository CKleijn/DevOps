using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class MailAdapter : INotificationAdapter
    {
        private NotificationProvider _type { get; init; }
        public NotificationProvider Type { get => _type; init => _type = value; }

        public MailAdapter()
        {
            Type = NotificationProvider.MAIL;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) 
        {
            new Mail().SendMail(notification.Title, notification.Body, notification.Recipient!.Email);
        }
    }
}

