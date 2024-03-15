using System.Text;
using Domain.Enums;

namespace Domain.Entities;

public class Notification
{
    private Guid _id { get; init; }
    private Guid Id { get => _id; init => _id = value; }

    private string _title { get; set; }
    public string Title { get => _title; set => _title = value; }

    private string _body { get; set; }
    public string Body { get => _body; set => _body = value; }
    
    private bool _status { get; set; }
    public bool Status { get => _status; set => _status = value; }
    
    private List<User> _targetUsers { get; set; }
    public List<User> TargetUsers { get => _targetUsers; set => _targetUsers = value; }
    
    private List<NotificationProvider> _destinationTypes { get; set; }
    public List<NotificationProvider> DestinationTypes { get => _destinationTypes; set => _destinationTypes = value; }
    
    private DateTime? _updatedAt { get; set; }
    public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
    
    private DateTime _createdAt { get; init; }
    public DateTime CreatedAt { get => _createdAt; init => _createdAt = value;}
    
    public Notification(string title, string body, bool status, List<User> targetUsers, List<NotificationProvider> destinationTypes)
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
        
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"Body: {Body}");
        sb.AppendLine($"Status: {Status}");
        sb.AppendLine($"UpdatedAt: {UpdatedAt}");
        sb.AppendLine($"CreatedAt: {CreatedAt}");
        
        return sb.ToString();
    }
}