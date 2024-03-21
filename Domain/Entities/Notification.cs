using System.Text;
using Domain.Helpers;
using Domain.Services;

namespace Domain.Entities;

public class Notification
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }

    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }

    private string _body { get; set; }
    public string Body { get => _body; set => _body = value; }
    
    private IList<User> _targetUsers { get; set; }
    public IList<User> TargetUsers { get => _targetUsers; set => _targetUsers = value; }
    
    private User? _recipient { get; set; }
    public User? Recipient { get => _recipient; set => _recipient = value; }

    private NotificationService _notificationService { get; init; }
    
    public Notification(string title, string body)
    {
        _id = Guid.NewGuid();
        _title = title;
        _body = body;
        _targetUsers = new List<User>();
        _notificationService = new NotificationService(this);

        Logger.DisplayCreatedAlert(nameof(Notification), _title);
    }
    
    public void AddTargetUser(User user)
    {
        _targetUsers.Add(user);

        Logger.DisplayAddedAlert(nameof(Notification), user.Name);
    }
    
    public void RemoveTargetUser(User user)
    {
        _targetUsers.Remove(user);

        Logger.DisplayRemovedAlert(nameof(Notification), user.Name);
    }

    public void SendNotification()
    {
        _notificationService.SendNotification();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Body: {_body}");
        sb.AppendLine($"Target users: {_targetUsers.Count}");

        foreach (var targetUser in _targetUsers)
        {
            sb.AppendLine(targetUser.ToString());
        }

        return sb.ToString();
    }
}