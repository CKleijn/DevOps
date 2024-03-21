using System.Text;
using Domain.Enums;
using Domain.Helpers;
using Domain.Interfaces.Observer;

namespace Domain.Entities;

public abstract class User : IObserver
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }
    private string _name { get; set; }
    public string Name { get => _name; set => _name = value; }
    
    private string _email { get; set; }
    public string Email { get => _email; set => _email = value; }
    
    private string _password { get; set; }
    public string Password { get => _password; set => _password = value; }

    private List<NotificationProvider> _destinationTypes { get; set; }
    public List<NotificationProvider> DestinationTypes { get => _destinationTypes; set => _destinationTypes = value; }

    public User(string name, string email, string password, List<NotificationProvider> notificationProviders)
    {
        _id = Guid.NewGuid();
        _name = name;
        _email = email;
        _password = password;
        _destinationTypes = notificationProviders;
    }

    public void AddDestinationType(NotificationProvider destinationType)
    {
        _destinationTypes.Add(destinationType);

        Logger.DisplayAddedAlert(nameof(User), Enum.GetName(typeof(NotificationProvider), destinationType)!);
    }

    public void RemoveDestinationType(NotificationProvider destinationType)
    {
        _destinationTypes.Remove(destinationType);

        Logger.DisplayRemovedAlert(nameof(User), Enum.GetName(typeof(NotificationProvider), destinationType)!);
    }

    public void Update(Notification notification)
    {
        notification.Recipient = this;
        notification.SendNotification();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Name: {_name}");
        sb.AppendLine($"Email: {_email}");
        sb.AppendLine($"Destination Types: {_destinationTypes.Count}");

        foreach (var destinationType in _destinationTypes)
        {
            sb.AppendLine(Enum.GetName(typeof(NotificationProvider), destinationType));
        }

        return sb.ToString();
    }
}