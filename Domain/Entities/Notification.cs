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
    
    private IList<NotificationProvider> _destinationTypes { get; set; }
    public IList<NotificationProvider> DestinationTypes { get => _destinationTypes; set => _destinationTypes = value; }
    
    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Notification(string title, string body, bool status, IList<User> targetUsers, IList<NotificationProvider> destinationTypes)
    {
        Id = Guid.NewGuid();
        Title = title;
        Body = body;
        Status = status;
        TargetUsers = targetUsers;
        DestinationTypes = destinationTypes;
        CreatedAt = DateTime.Now;
    }
    
    //TODO: implement further functions
    
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Id: {_id}");
        sb.AppendLine($"Title: {_title}");
        sb.AppendLine($"Body: {_body}");
        sb.AppendLine($"Status: {_status}");
        sb.AppendLine($"UpdatedAt: {_updatedAt}");
        sb.AppendLine($"CreatedAt: {_createdAt}");
        
        return sb.ToString();
    }
}