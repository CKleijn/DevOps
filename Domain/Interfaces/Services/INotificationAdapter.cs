using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services;

public interface INotificationAdapter
{
    public NotificationProvider Type { get; init; }
    public void SendMessage(Notification notification);
}