using System.Text;
using Domain.Enums;
using Domain.Helpers;

namespace Domain.Entities;

public class Notification
{
    private Guid _id { get; init; }
    public Guid Id { get => _id; init => _id = value; }

    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }

    private string _body { get; set; }
    public string Body { get => _body; set => _body = value; }
    
    private bool _status { get; set; }
    public bool Status { get => _status; set => _status = value; }
    
    private IList<User> _targetUsers { get; set; }
    public IList<User> TargetUsers { get => _targetUsers; set => _targetUsers = value; }
    
    private List<NotificationProvider> _destinationTypes { get; set; }
    public List<NotificationProvider> DestinationTypes { get => _destinationTypes; set => _destinationTypes = value; }
    
    public Notification(string title, string body, bool status)
    {
        _id = Guid.NewGuid();
        _title = title;
        _body = body;
        _status = status;
        _targetUsers = new List<User>();
        _destinationTypes = new List<NotificationProvider>();

        Logger.DisplayCreatedAlert(nameof(Notification), _title);
    }
    
    public void AddTargetUser(User user)
    {
        _targetUsers.Add(user);

        Logger.DisplayUpdatedAlert(nameof(Notification), $"Added: {user}");
    }
    
    public void AddDestinationType(NotificationProvider destinationType)
    {
        _destinationTypes.Add(destinationType);

        Logger.DisplayUpdatedAlert(nameof(Notification), $"Added: {destinationType}");
    }
    
    //TODO: implement further functions
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Body: {_body}");
        sb.AppendLine($"Status: {_status}");
        
        return sb.ToString();
    }
}