using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Services;
using Infrastructure.Libraries.Notification;

namespace Infrastructure.Adapters.Notification
{
    public class MailAdapter : INotificationAdapter
    {
        public NotificationProvider Type { get; init; }
    
        public MailAdapter()
        {
            Type = NotificationProvider.MAIL;
        }
    
        public void SendMessage(Domain.Entities.Notification notification) {
            
            //send mail to all targeted users
            foreach(var recipient in notification.TargetUsers) {
                new Mail().SendMail(notification.Title, notification.Body, recipient.Email, notification.Sender.Email);
            }
        }
    }
}

