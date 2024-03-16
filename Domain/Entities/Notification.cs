using System.Text;
using Domain.Enums;

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
    
    private IList<NotificationProvider> _destinationTypes { get; init; }
    public IList<NotificationProvider> DestinationTypes { get => _destinationTypes; init => _destinationTypes = value; }
    
    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Notification(string title, string body, bool status)
    {
        _id = Guid.NewGuid();
        _title = title;
        _body = body;
        _status = status;
        _targetUsers = new List<User>();
        _destinationTypes = new List<NotificationProvider>();
        _createdAt = DateTime.Now;
    }
    
    public void AddTargetUser(User user)
    {
        _targetUsers.Add(user);
    }
    
    public void AddDestinationType(NotificationProvider destinationType)
    {
        _destinationTypes.Add(destinationType);
    }
    
    //TODO: implement further functions
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"Body: {Body}");
        sb.AppendLine($"Status: {Status}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");
        
        return sb.ToString();
    }
}